using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MemoryStore
{
    public static class Store
    {
        private static List<Order> OrdersMemoryCollection = new List<Order>();

        private static void WriteToFile(Order order)
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string jsonFileName = Path.Combine(assemblyFolder, "orders.json");
            List<Order> orders = ReadFromFile();
            using (StreamWriter file = File.CreateText(jsonFileName))
            {
                orders.Add(order);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, orders);
            }
        }

        private static List<Order> ReadFromFile()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string jsonFileName = Path.Combine(assemblyFolder, "orders.json");
            using (StreamReader file = File.OpenText(jsonFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                var result = (List<Order>)serializer.Deserialize(file, typeof(List<Order>));

                if (result != null)
                {
                    return result;
                }

                return new List<Order>();
            }
            
        }

        private static void AddToMemoryCollection(Order order)
        {
            OrdersMemoryCollection.Add(order);
        }

        private static List<Order> ReadFromMemoryCollection()
        {
            return OrdersMemoryCollection;
        }

        public static void Add(Order order, bool addToMemory)
        {
            if(addToMemory)
            {
                AddToMemoryCollection(order);
            }
            WriteToFile(order);
        }

        public static List<Order> Read(bool addToMemory)
        {
            if (addToMemory)
            {
                return ReadFromMemoryCollection();
            }

            return ReadFromFile();
        }

        public static void Update(Order order, bool addToMemory)
        {
            var order1 = Read(addToMemory).FirstOrDefault(x => x.OrderId == order.OrderId);
            order1.OrderTests = order.OrderTests;
            order1.IsCanceledOrder = order.IsCanceledOrder;

            if (!addToMemory)
            {
                Add(order1, addToMemory);
            }

        }
    }
}
