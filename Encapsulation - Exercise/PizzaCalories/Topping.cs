using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Topping
    {
        private Dictionary<string, double> typeOfTopping = new Dictionary<string, double>()
        {
            ["meat"] = 1.2,
            ["veggies"] = 0.8,
            ["cheese"] = 1.1,
            ["sauce"] = 0.9
        };

        private double modifire;

        private string type;
        private double grams;

        public Topping(string type, double grams)
        {
            Type = type;
            Grams = grams;
        }

        public double Grams
        {
            get { return grams; }
            private set
            {
                if (value < 1 || value > 50)
                    throw new ArgumentException($"{Type} weight should be in the range [1..50].");
                grams = value;
            }
        }


        public string Type
        {
            get { return type; }
            private set
            {
                if (value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                type = value;

                modifire = typeOfTopping[value.ToLower()];
            }
        }

        public double TotalCalories()
            => (2 * Grams) * modifire;
    }
}
