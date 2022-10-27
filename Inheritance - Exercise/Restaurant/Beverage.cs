using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class Beverage : Product
    {
        public Beverage(string name, decimal price, double мillileters)
            : base (name, price)
        {
            Millileters = мillileters;
        }

        public double Millileters { get; set; }
    }
}
