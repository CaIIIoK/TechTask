using Models;
using System;
using System.Collections.Generic;

namespace CustomLibrary.Interfaces
{
    public interface ILibService
    {
        bool AddOrder(Order order);

        bool DeleteOrder(int orderId);

        bool CancelOrder(int orderId);

        bool CancelTest(int orderId, int testId);

        Order GetOrderById(int orderId);

        void AddTests(int orderId, List<Test> tests);

        List<Order> GetAllOrders();
    }
}
