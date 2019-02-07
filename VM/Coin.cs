using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    class Coin
    {
        public int coinValue { get; set; }
        public int quantity { get; set; }
        public Coin() { }
        public Coin(int coinValue, int quantity)
        {
            this.coinValue = coinValue;
            this.quantity = quantity;
        }
        
    }

}
