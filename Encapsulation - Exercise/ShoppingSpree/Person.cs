using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        public List<string> BagOfProducts { get; set; }

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            BagOfProducts = new List<string>();
        }

        public decimal Money
        {
            get { return money; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Money cannot be negative");
                money = value;
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                name = value;
            }
        }

        public override string ToString()
        {
            if (BagOfProducts.Count > 0)
                return $"{Name} - {string.Join(", ", BagOfProducts)}";

            return $"{Name} - Nothing bought";
        }
    }
}
