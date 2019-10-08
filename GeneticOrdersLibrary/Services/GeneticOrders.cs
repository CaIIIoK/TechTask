using CustomLibrary.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DataStore;
using CustomLibrary.Models;

namespace CustomLibrary.Services
{
    public class GeneticOrders : IGeneticOrders
    {
        private readonly IStore _store;

        public GeneticOrders(IStore store)
        {
            _store = store;
        }

        public Response AddOrder(Order order)
        {
            bool areOrderTestsValid = order.OrderTests != null && !order.OrderTests.Any(x => x.TestId <= 0) 
                && order.OrderTests.Any();

            int testIds = order.OrderTests.Select(x => x.TestId).Distinct().Count();
            bool areTestsInOrderUnique = testIds == order.OrderTests.Count();

            if (order != null && areOrderTestsValid && areTestsInOrderUnique)
            {
                if (!IsOrderAlreadyExists(order.OrderId))
                {
                        _store.Add(order);

                        return new Response() { ResponseType = ResponseType.Success, Description = "Order is added" };
                }
            }

            return new Response() { ResponseType = ResponseType.Failed, Description = "Order isn't added" };
        }

        public List<Order> GetAllOrders()
        {
            return _store.Read();
        }

        public bool CancelOrder(int orderId)
        {
            if(IsOrderValid(orderId))
            {
                Order order = GetOrderById(orderId);

                if(!order.IsCanceledOrder)
                {
                    order.IsCanceledOrder = true;
                    _store.Update(order);
                    return true;
                }
            }

            return false;
        }

        public Order GetOrderById(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("Order ID cant be negative or zero!");
            }

            return _store.Read().FirstOrDefault(x => x.OrderId == orderId);
        }

        public bool AddTests(int orderId, List<Test> tests)
        {
            int testIds = tests.Select(x => x.TestId).Distinct().Count();
            bool areInputTestsUnique = testIds == tests.Count();

            if (!IsOrderValid(orderId) || tests.Any(x => x.TestId <= 0) || !areInputTestsUnique)
            {
                return false;
            }

            Order order = GetOrderById(orderId);
            order.OrderTests.AddRange(tests);
            _store.Update(order);
                     
            return true;
        }

        private bool IsOrderValid(int orderId)
        {
            if (orderId < 0)
            {
                return false;
            }

            Order order = GetOrderById(orderId);

            if (order == null)
            {
                return false;
            }            

            return true;
        }

        public bool CancelTest(int orderId, int testId)
        {
            if(!IsOrderValid(orderId))
            {
                return false;
            }
            Order order = GetOrderById(orderId);
            Test test = order.OrderTests.FirstOrDefault(x => x.TestId == testId);

            if (test == null)
            {
                throw new NullReferenceException("Test can't be NULL");
            }

            bool IsTestCanceled = test.IsCanceledTest;

            if (!IsTestCanceled)
            {
                test.IsCanceledTest = true;

                if (AreAllTestsInOrderCanceled(order.OrderTests))
                {
                    order.IsCanceledOrder = true;
                }

                _store.Update(order);
                return true;
            }

            return false;
        }

        private bool AreAllTestsInOrderCanceled(List<Test> tests)
        {
            return tests.All(x => x.IsCanceledTest);
        }

        private bool IsOrderAlreadyExists(int orderId)
        {
            List<Order> orders = GetAllOrders();
            Order searchingOrder = orders.FirstOrDefault(x => x.OrderId == orderId);

            if (searchingOrder != null)
            {
                return true;
            }

            return false;
        }
    }
}
