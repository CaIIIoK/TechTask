using Models;

namespace DataStore.Common
{
    public static class Mapping
    {
        public static void MapOrders(Order destination, Order source)
        {
            destination.OrderTests = source.OrderTests;
            destination.IsCanceledOrder = source.IsCanceledOrder;
        }
    }
}
