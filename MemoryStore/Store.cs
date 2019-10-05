using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryStore
{
    public static class Store
    {
        public static List<Order> OrdersMemoryCollection = new List<Order>() {
            new Order {
                OrderId = 1,
                IsCanceledOrder = false,
                OrderTests = new List<Test>() {
                    new Test {
                        TestId = 1,
                        Name = "DNA1"                    
                    },
                    new Test {
                        TestId = 2,
                        Name = "DNA2"

                    }
                }
            },

            new Order {
                OrderId = 2,
                IsCanceledOrder = true,
                OrderTests = new List<Test>() {
                    new Test {
                        TestId = 3,
                        Name = "Serology"
                    },
                    new Test {
                        TestId = 4,
                        Name = "Toxicology"
                    }
                }
            },
        };
    }
}
