using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class Cake : Dessert
    {
        public Cake(string name) : base(name, price, grams, calories)
        {
            Grams = grams;
            Calories = calories;
            Price = price;
        }

        public const double grams = 250;
        public const double calories = 1000;
        public const decimal price = 5;
    }
}
