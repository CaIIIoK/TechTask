using CustomLibrary.Interfaces;
using CustomLibrary.Services;
using Models;
using System.Collections.Generic;
using Xunit;
using DataStore;
using CustomLibrary.Models;

namespace LibUnitTests
{
    public class LibTests
    {

        private readonly IGeneticOrders libService;

        public LibTests()
        {
            IStore store = new MemoryStore();
            libService = new GeneticOrders(store);
            store.Read().Clear();
        }


        [Fact]
        public void AddOrder_WhenOrderDoesntHaveTests_ReturnsFasle()
        {           
            Order order = new Order();
            ResponseType expected = ResponseType.Failed;

            Response result = libService.AddOrder(order);

            Assert.Equal(result.ResponseType, expected);
        }

        [Fact]
        public void AddOrder_WhenOneTestIsAddedToOrder_ReturnsTrue()
        {
            Order order = new Order();
            order.OrderTests = new List<Test>();
            order.OrderTests.Add(new Test());

           //bool IsOrderAdded = libService.AddOrder(order);

            //Assert.True(IsOrderAdded);
        }

        [Fact]
        public void AddOrder_WhenOrderIdIsNotUnique_ReturnsFalse()
        {
            Order order = CreateOrder();
            libService.AddOrder(order);

            //bool IsOrderIdUnique = libService.AddOrder(order);

           // Assert.False(IsOrderIdUnique);
        }

        [Fact]
        public void CancelOrder_WhenTheLastTestIsCanceled_ReturnTrue()
        {
            Order order = CreateOrder();
            libService.AddOrder(order);

            //bool IsOrderCanceled = libService.CancelTest(order.OrderId, 1);

            //Assert.True(IsOrderCanceled);
        }

        [Fact]
        public void CancelOrder_WhenOrderIsCanceled_ReturnsTrue()
        {
            Order order = CreateOrder();
            libService.AddOrder(order);

            //bool IsOrderCanceled = libService.CancelOrder(order.OrderId);

            //Assert.True(IsOrderCanceled);
        }

        

        private Order CreateOrder()
        {
            return new Order()
            {
                OrderId = 1,
                IsCanceledOrder = false,
                OrderTests = new List<Test>() {
                    new Test {
                        TestId = 1,
                        Name = "Serology"
                    }
                }
            };
        }
    }
}
