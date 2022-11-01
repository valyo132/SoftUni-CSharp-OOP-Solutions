using System;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            bool makingPizza = false;
            Pizza pizza = null;

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] tokens = command.Split(" ");
                    string product = tokens[0];

                    if (product == "Dough")
                    {
                        string type = tokens[1];
                        string bakingTech = tokens[2];
                        double weigh = double.Parse(tokens[3]);

                        Dough dough = new Dough(type, bakingTech, weigh);
                        Console.WriteLine($"{dough.TotalCalories():f2}");
                    }
                    else if (product == "Topping")
                    {
                        string type = tokens[1];
                        double weigh = double.Parse(tokens[2]);

                        Topping topping = new Topping(type, weigh);

                        if (makingPizza)
                        {
                            pizza.AddTopping(topping);
                        }
                        else
                        {
                            Console.WriteLine($"{topping.TotalCalories():f2}");
                        }
                    }
                    else if (product == "Pizza")
                    {
                        makingPizza = true;
                        string name = tokens[1];

                        string[] doughForThePizza = Console.ReadLine().Split(" ");
                        string type = doughForThePizza[1];
                        string bakingTech = doughForThePizza[2];
                        double weigh = double.Parse(doughForThePizza[3]);

                        Dough dough = new Dough(type, bakingTech, weigh);

                        pizza = new Pizza(name, dough);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalPizzaCalories():f2} Calories.");
        }
    }
}
