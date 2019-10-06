﻿using CustomLibrary.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MemoryStore;

namespace CustomLibrary.Services
{
    public class LibService : ILibService
    {

        public bool AddOrder(Order order)
        {
            if ((order != null)&&(order.OrderTests != null))
            {
                if(!IsOrderAlreadyExists(order.OrderId))
                {
                    if (order.OrderTests.Any())
                    {
                        Store.OrdersMemoryCollection.Add(order);
                        return true;
                    }
                }
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
            if(orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("Order ID cant be negative or zero!");
            }
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
            Test test = order.OrderTests.FirstOrDefault(x => x.TestId == testId);
            if(test == null)
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
                return true;
            }
            return false;
        }

        public bool AreAllTestsInOrderCanceled(List<Test> tests)
        {
            foreach (Test test in tests)
            {
                if (!test.IsCanceledTest)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsOrderAlreadyExists(int orderId)
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
