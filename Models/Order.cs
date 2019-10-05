using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public List<Test> OrderTests { get; set; }

        public bool IsCanceledOrder { get; set; }

        
    }
}
