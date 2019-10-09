using Models;
using System.Collections.Generic;

namespace DataStore
{
    public interface IStore
    {
        void Add(Order order);
        List<Order> Read();
        void Update(Order order);
    }
}
