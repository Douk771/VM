using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    class Product
    {
        public string position { get; set; }
        public int cost { get; set; }
        public int quantity { get; set; }
        public Product() { }
        public Product(string position, int cost, int quantity)
        {
            this.position = position;
            this.cost = cost;
            this.quantity = quantity;
        }
    }
}
