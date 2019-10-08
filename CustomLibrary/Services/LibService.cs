using CustomLibrary.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MemoryStore;
using CustomLibrary.Models;

namespace CustomLibrary.Services
{
    public class LibService : ILibService
    {
        bool _IsMemoryStore;

        public LibService(bool IsMemoryStore)
        {
            _IsMemoryStore = IsMemoryStore;
        }

        public Response AddOrder(Order order)
        {
            bool areOrderTestsValid = order.OrderTests != null && !order.OrderTests.Any(x => x.TestId <= 0);

            //вынести
            int testIds = order.OrderTests.Select(x => x.TestId).Distinct().Count();
            bool areTestsInOrderUnique = testIds == order.OrderTests.Count();

            if (order != null && areOrderTestsValid && areTestsInOrderUnique)
            {
                if (!IsOrderAlreadyExists(order.OrderId))
                {
                    if (order.OrderTests.Any())
                    {
                        Store.Add(order, _IsMemoryStore);

                        return new Response() { ResponseType = ResponseType.Success, Description =  "Order is added" };
                    }
                }
            }

            return new Response() {ResponseType = ResponseType.Failed, Description = "Order isn't added" };
        }

        public List<Order> GetAllOrders()
        {
            return Store.Read(_IsMemoryStore);
        }

        public bool CancelOrder(int orderId)
        {
            Order order = ValidateOrderIdInColletion(orderId);
            bool IsOrderCanceled = order.IsCanceledOrder;

            if (!IsOrderCanceled)
            {
                order.IsCanceledOrder = !order.IsCanceledOrder;
                Store.Update(order, _IsMemoryStore);
                return true;
            }

            return false;
        }

        public Order GetOrderById(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("Order ID cant be negative or zero!");
            }

            return Store.Read(_IsMemoryStore).FirstOrDefault(x => x.OrderId == orderId);
        }

        public void AddTests(int orderId, List<Test> tests)
        {
            Order order = ValidateOrderIdInColletion(orderId);
            order.OrderTests.AddRange(tests);
        }

        private Order ValidateOrderIdInColletion(int orderId)
        {
            if (orderId < 0)
            {
                throw new ArgumentOutOfRangeException("Order ID couldn't be negative");
            }

            Order order = GetOrderById(orderId);

            if (order == null)
            {
                //change something like DataNotFoundException
                throw new NullReferenceException("Order cann't be NULL!");
            }

            bool IsTestIdLessThanZero = order.OrderTests.Any(x => x.TestId <= 0);

            if(IsTestIdLessThanZero)
            {
                throw new ArgumentOutOfRangeException("Test ID couldn't be negative");
            }

            return order;
        }

        public bool CancelTest(int orderId, int testId)
        {
            Order order = ValidateOrderIdInColletion(orderId);
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

                Store.Update(order, _IsMemoryStore);
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
