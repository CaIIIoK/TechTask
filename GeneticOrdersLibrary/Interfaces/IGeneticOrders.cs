using CustomLibrary.Models;
using Models;
using System.Collections.Generic;

namespace CustomLibrary.Interfaces
{
    public interface IGeneticOrders
    {
        Response AddOrder(Order order);

        Response CancelOrder(int orderId);

        Response CancelTest(int orderId, int testId);

        Order GetOrderById(int orderId);

        Response AddTests(int orderId, List<Test> tests);

        List<Order> GetAllOrders();
    }
}
