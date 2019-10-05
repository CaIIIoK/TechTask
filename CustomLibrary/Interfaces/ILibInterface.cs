using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLibrary.Interfaces
{
    public interface ILibService
    {
        bool AddOrder(Order order);

        bool DeleteOrder(int orderId);

        bool CancelOrder(int orderId);

        Order GetOrderById(int orderId);

        void AddTests(int orderId, List<Test> tests);

        List<Order> GetAllOrders();
    }
}
