using DataStore.Common;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DataStore
{
    public class MemoryStore : IStore
    {

        private static List<Order> ordersMemoryCollection = new List<Order>();


        private static void AddToMemoryCollection(Order order)
        {
            ordersMemoryCollection.Add(order);
        }

        private static List<Order> ReadFromMemoryCollection()
        {
            return ordersMemoryCollection;
        }

        public void Add(Order order)
        {
            AddToMemoryCollection(order);
        }

        public List<Order> Read()
        {
            return ReadFromMemoryCollection();
        }

        public void Update(Order order)
        {
            List<Order> orders = Read();
            var orderInStore = orders.FirstOrDefault(x => x.OrderId == order.OrderId);

            if (orderInStore != null)
            {
                Mapping.MapOrders(orderInStore, order);
            }
        }      
    }
}
