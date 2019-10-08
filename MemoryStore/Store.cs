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
        private const string fileName = "orders.json";

        private static string jsonFileName;

        private static List<Order> OrdersMemoryCollection = new List<Order>();

        static Store()
        {
            string assemblyFolder = GetAssemblyFolder();
            jsonFileName = GetJsonFileName(assemblyFolder);
        }

        private static void WriteToFile(Order order)
        {
            List<Order> orders = ReadFromFile();
            using (StreamWriter file = File.CreateText(jsonFileName))
            {
                orders.Add(order);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, orders);
            }
        }

        private static void WriteToFile(List<Order> orders)
        {
            using (StreamWriter file = File.CreateText(jsonFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, orders);
            }
        }

        private static string GetAssemblyFolder()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static string GetJsonFileName(string assemblyFolder)
        {
            return Path.Combine(assemblyFolder, fileName);
        }

        private static List<Order> ReadFromFile()
        {
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
            if (addToMemory)
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
            List<Order> orders = Read(addToMemory);
            var orderInStore = orders.FirstOrDefault(x => x.OrderId == order.OrderId);

            if (orderInStore != null)
            {
                MapOrders(orderInStore, order);

                if (!addToMemory)
                {
                    WriteToFile(orders);
                }
            }
        }

        private static void MapOrders(Order destination, Order source)
        {
            destination.OrderTests = source.OrderTests;
            destination.IsCanceledOrder = source.IsCanceledOrder;
        }
    }
}
