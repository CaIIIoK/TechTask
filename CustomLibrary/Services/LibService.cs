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
            //TO DO - DONE
            //Check if order has at least one test
           if(order.OrderTests.Any<Test>())
            {

                Store.OrdersMemoryCollection.Add(order);
                return true;

                // TO DO Add new logic to check order status

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
            //TO DO - DONE
            //Add validation for orderId < 0
            if (orderId < 0)
            {
                throw new ArgumentOutOfRangeException("Order ID couldn't be negative");
            }
            Order order = GetOrderById(orderId);
            if (order == null)
            {
                throw new NullReferenceException("Order cann't be NULL!");
            }

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
            Order order = GetOrderById(orderId);
            if (order == null)
            {
                throw new NullReferenceException("Order cann't be NULL!");
            }

            order.OrderTests.AddRange(tests);
        }
    }
}
