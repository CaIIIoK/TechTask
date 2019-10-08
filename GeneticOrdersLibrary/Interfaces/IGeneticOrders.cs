using CustomLibrary.Models;
using Models;
using System.Collections.Generic;

namespace CustomLibrary.Interfaces
{
    public interface IGeneticOrders
    {
        Response AddOrder(Order order);

        bool CancelOrder(int orderId);

        bool CancelTest(int orderId, int testId);

        Order GetOrderById(int orderId);

        bool AddTests(int orderId, List<Test> tests);

        List<Order> GetAllOrders();
    }
}
