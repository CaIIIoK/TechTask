using CustomLibrary.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MemoryStore;

namespace CustomLibrary.Services
{
    public class LibService : ILibService
    {
        
        public bool AddOrder(Order order)
        {            
           if(order.OrderTests.Any())
            {
                Store.OrdersMemoryCollection.Add(order);
                return true;
            }            
            return false;
        }

        public List<Order> GetAllOrders()
        {
            return Store.OrdersMemoryCollection;
        }

        public bool DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }
              
        public bool CancelOrder(int orderId)
        {
            Order order = ValidateOrderIdInColletion(orderId);
            bool IsOrderCanceled = order.IsCanceledOrder;
            if (!IsOrderCanceled)
            {
                order.IsCanceledOrder = true;
                return true;
            }
            return false;
        }

        public Order GetOrderById(int orderId)
        {
            return Store.OrdersMemoryCollection.FirstOrDefault(x => x.OrderId == orderId);
        }

        public void AddTests(int orderId, List<Test> tests)
        {
            Order order = ValidateOrderIdInColletion(orderId);            
            order.OrderTests.AddRange(tests);
        }

        public Order ValidateOrderIdInColletion(int orderId)
        {
            if (orderId < 0)
            {
                throw new ArgumentOutOfRangeException("Order ID couldn't be negative");
            }
            Order order = GetOrderById(orderId);
            if (order == null)
            {
                throw new NullReferenceException("Order cann't be NULL!");
            }
            return order;
        }

        public bool CancelTest(int orderId, int testId)
        {
            Order order = ValidateOrderIdInColletion(orderId);
            Test test = order.OrderTests.Find(x => x.TestId == testId);            
            bool IsTestCanceled = test.IsCanceledTest;
            if(!IsTestCanceled)
            {
                test.IsCanceledTest = true;
                return true;
            }            
            return false;
        }
    }
}
