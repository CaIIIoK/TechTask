using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<Test> Tests { get; set; }

        public bool IsCanceled { get; set; }

        
    }
}
