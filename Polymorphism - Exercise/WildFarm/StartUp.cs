namespace WildFarm
{
    using System;
    using System.Collections.Generic;

    using WildFarm.Models.AnimalClasses;
    using WildFarm.Models.FoodClasses;

    public class StartUp
    {
        private static Animal animal = null;
        private static Food food = null;

        static void Main(string[] args)
        {
            var allAnimals = new List<Animal>();

            //int line = 0;

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] animalInfo = command.Split(); //
                string type = animalInfo[0];

                animal = AnimalFactory.CreateAnimal(type, animalInfo);
                Console.WriteLine(animal.Talk());

                allAnimals.Add(animal);

                string[] foodForAnimal = Console.ReadLine().Split(); //
                string foodType = foodForAnimal[0];
                int foodQuantity = int.Parse(foodForAnimal[1]);

                food = AnimalFactory.CreateFood(foodType, foodQuantity);

                if (CanAnimalEatFood())
                    FeedAnimal();
                else
                    Console.WriteLine($"{animal.GetType().Name} does not eat {food.GetType().Name}!");
            }

            foreach (var item in allAnimals)
            {
                Console.WriteLine(item);
            }
        }

        private static void FeedAnimal()
        {
            animal.FoodEaten += food.Quantity;

            string typeAnimal = animal.GetType().Name;
            switch (typeAnimal)
            {
                case "Hen": animal.Weight += 0.35 * food.Quantity; break;
                case "Owl": animal.Weight += 0.25 * food.Quantity; break;
                case "Mouse": animal.Weight += 0.10 * food.Quantity; break;
                case "Cat": animal.Weight += 0.30 * food.Quantity; break;
                case "Dog": animal.Weight += 0.40 * food.Quantity; break;
                case "Tiger": animal.Weight += 1.00 * food.Quantity; break;
            }
        }

        private static bool CanAnimalEatFood()
        {
            string typeAnimal = animal.GetType().Name;
            string foodType = food.GetType().Name;

            if (typeAnimal == "Hen")
            {
                return true;
            }
            else if (typeAnimal == "Mouse" && (foodType == "Vegetable" || foodType == "Fruit"))
            {
                return true;
            }
            else if (typeAnimal == "Cat" && (foodType == "Vegetable" || foodType == "Meat"))
            {
                return true;
            }
            else if (typeAnimal == "Dog" && foodType == "Meat")
            {
                return true;
            }
            else if (typeAnimal == "Owl" && foodType == "Meat")
            {
                return true;
            }
            else if (typeAnimal == "Tiger" && foodType == "Meat")
            {
                return true;
            }

            return false;
        }
    }
}
