using CustomLibrary.Interfaces;
using CustomLibrary.Services;
using Models;
using System.Collections.Generic;
using Xunit;

namespace LibUnitTests
{
    public class LibTests
    {

        private readonly ILibService libService;

        public LibTests()
        {
            libService = new LibService();
        }

        [Fact]
        public void AddOrder_WhenOrderDoesntHaveTests_ReturnsFasle()
        {           
            Order order = new Order();

            bool IsOrderAdded = libService.AddOrder(order);

            Assert.False(IsOrderAdded);
        }

        [Fact]
        public void AddOrder_WhenOneTestIsAddedToOrder_ReturnsTrue()
        {
            Order order = new Order();
            order.OrderTests = new List<Test>();
            order.OrderTests.Add(new Test());

            bool IsOrderAdded = libService.AddOrder(order);

            Assert.True(IsOrderAdded);
        }
    }
}
