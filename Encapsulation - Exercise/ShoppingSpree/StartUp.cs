using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var allPeople = new List<Person>();
            var allProducts = new List<Product>();

            try
            {
                string[] peopleInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in peopleInput)
                {
                    string[] tokens = item.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    string name = tokens[0];
                    decimal money = decimal.Parse(tokens[1]);

                    Person person = new Person(name, money);
                    allPeople.Add(person);
                }

                string[] productsInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in productsInput)
                {
                    string[] tokens = item.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    string name = tokens[0];
                    decimal cost = decimal.Parse(tokens[1]);

                    Product product = new Product(name, cost);
                    allProducts.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string command;
            while ((command = Console.ReadLine()) != "END") 
            {
                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string client = tokens[0];
                string productToBuy = tokens[1];

                if (allPeople.Any(x => x.Name == client))
                {
                    var person = allPeople.First(x => x.Name == client);

                    if (allProducts.Any(x => x.Name == productToBuy))
                    {
                        var product = allProducts.First(x => x.Name == productToBuy);

                        if (person.Money < product.Cost)
                        {
                            Console.WriteLine($"{person.Name} can't afford {product.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"{person.Name} bought {product.Name}");
                            person.BagOfProducts.Add(product.Name);
                            person.Money -= product.Cost;
                        }
                    }
                }
            }

            foreach (var person in allPeople)
                Console.WriteLine(person);
        }
    }
}
