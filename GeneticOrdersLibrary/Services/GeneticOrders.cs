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
            bool areOrderTestsValid = order.Tests != null && !order.Tests.Any(x => x.Id <= 0) 
                && order.Tests.Any();
            int testIds = order.Tests.Select(x => x.Id).Distinct().Count();
            bool areTestsInOrderUnique = testIds == order.Tests.Count();

            if (order != null && areOrderTestsValid && areTestsInOrderUnique)
            {
                if (!IsOrderAlreadyExists(order.Id))
                {
                        _store.Add(order);

                        return new Response() { ResponseType = ResponseType.Success, Description = "Order is added." };
                }
            }

            return new Response() { ResponseType = ResponseType.Failed, Description = "Order isn't added." };
        }

        public List<Order> GetAllOrders()
        {
            return _store.Read();
        }

        public Response CancelOrder(int orderId)
        {
            if(IsOrderValid(orderId))
            {
                Order order = GetOrderById(orderId);

                if(!order.IsCanceled)
                {
                    order.IsCanceled = true;
                    _store.Update(order);

                    return new Response() { ResponseType = ResponseType.Success, Description = "Order is canceled." };
                }
            }

            return new Response() { ResponseType = ResponseType.Failed, Description = "Order isn't canceled." };
        }

        public Order GetOrderById(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("Order ID is negative or equals zero!");
            }

            return _store.Read().FirstOrDefault(x => x.Id == orderId);
        }

        public Response AddTests(int orderId, List<Test> tests)
        {
            int testIds = tests.Select(x => x.Id).Distinct().Count();
            bool areInputTestsUnique = testIds == tests.Count();

            if (!IsOrderValid(orderId) || tests.Any(x => x.Id <= 0) || !areInputTestsUnique)
            {
                return new Response() { ResponseType = ResponseType.Failed, Description = "Tests aren't added." };
            }

            Order order = GetOrderById(orderId);
            order.Tests.AddRange(tests);
            _store.Update(order);

            return new Response() { ResponseType = ResponseType.Success, Description = "Tests are added." };
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

        public Response CancelTest(int orderId, int testId)
        {
            if(!IsOrderValid(orderId))
            {
                return new Response() { ResponseType = ResponseType.Failed, Description = "Order isn't valid." };
            }

            Order order = GetOrderById(orderId);
            Test test = order.Tests.FirstOrDefault(x => x.Id == testId);

            if (test == null)
            {
                throw new Exception($"Test with specified Id {testId} isn't found in order!");
            }

            bool isTestCanceled = test.IsCanceled;

            if (!isTestCanceled)
            {
                test.IsCanceled = true;

                if (AreAllTestsInOrderCanceled(order.Tests))
                {
                    order.IsCanceled = true;
                }

                _store.Update(order);

                return new Response() { ResponseType = ResponseType.Success, Description = "Test is canceled." };
            }

            return new Response() { ResponseType = ResponseType.Failed, Description = "Test isn't canceled." };
        }

        private bool AreAllTestsInOrderCanceled(List<Test> tests)
        {
            return tests.All(x => x.IsCanceled);
        }

        private bool IsOrderAlreadyExists(int orderId)
        {
            List<Order> orders = GetAllOrders();
            return orders.Any(x => x.Id == orderId);
        }
    }
}
