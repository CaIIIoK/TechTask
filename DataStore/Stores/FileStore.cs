using DataStore.Common;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataStore
{
    public class FileStore : IStore
    {
        private const string FileName = "orders.json";

        private static string jsonFileName;

        public FileStore()
        {
            string assemblyFolder = GetAssemblyFolder();
            jsonFileName = GetJsonFileName(assemblyFolder);
        }

        public void Add(Order order)
        {
            WriteToFile(order);
        }

        public List<Order> Read()
        {
            return ReadFromFile();
        }

        public void Update(Order order)
        {
            List<Order> orders = Read();
            var orderInStore = orders.FirstOrDefault(x => x.OrderId == order.OrderId);

            if (orderInStore != null)
            {
                Mapping.MapOrders(orderInStore, order);
                WriteToFile(orders);
            }
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

        private static string GetAssemblyFolder()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static string GetJsonFileName(string assemblyFolder)
        {
            return Path.Combine(assemblyFolder, FileName);
        }
    }
}
