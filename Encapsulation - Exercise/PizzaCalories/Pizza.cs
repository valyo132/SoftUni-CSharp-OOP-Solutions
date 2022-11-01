using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            Name = name;
            Dough = dough;
            this.toppings = new List<Topping>();
        }

        public IReadOnlyCollection<Topping> Toppings
        {
            get { return toppings.AsReadOnly(); }
        }

        public Dough Dough
        {
            get { return dough; }
            private set { dough = value; }
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (value.Length < 1 || value.Length > 15 || string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                name = value;
            }
        }

        public void AddTopping(Topping topping)
        {
            if (Toppings.Count == 10)
                throw new ArgumentException("Number of toppings should be in range [0..10].");
           toppings.Add(topping);
        }

        public double TotalPizzaCalories()
        {
            double calories = 0;
            calories += dough.TotalCalories();

            foreach (Topping item in Toppings)
                 calories += item.TotalCalories();

            return calories;
        }
    }
}
