using Models;

namespace DataStore.Common
{
    public static class Mapping
    {
        public static void MapOrders(Order destination, Order source)
        {
            destination.Tests = source.Tests;
            destination.IsCanceled = source.IsCanceled;
        }
    }
}
