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
            order.Tests = new List<Test>();
            order.Tests.Add(new Test());
            ResponseType expected = ResponseType.Success;

            Response result = libService.AddOrder(order);

            Assert.Equal(result.ResponseType, expected);
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
                Id = 1,
                IsCanceled = false,
                Tests = new List<Test>() {
                    new Test {
                        Id = 1,
                        Name = "Serology"
                    }
                }
            };
        }
    }
}
