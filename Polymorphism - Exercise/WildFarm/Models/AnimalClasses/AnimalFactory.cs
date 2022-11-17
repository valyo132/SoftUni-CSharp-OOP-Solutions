namespace WildFarm.Models.AnimalClasses
{
    using System;
    using WildFarm.Models.FoodClasses;

    public class AnimalFactory
    {
        public static Animal CreateAnimal(string type, string[] animalInfo)
        {
            switch (type)
            {
                case "Cat":
                    return new Cat(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3], animalInfo[4]);
                case "Tiger":
                    return new Tiger(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3], animalInfo[4]);
                case "Owl":
                    return new Owl(animalInfo[1], double.Parse(animalInfo[2]), 0, double.Parse(animalInfo[3]));
                case "Hen":
                    return new Hen(animalInfo[1], double.Parse(animalInfo[2]), 0, double.Parse(animalInfo[3]));
                case "Mouse":
                    return new Mouse(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3]);
                case "Dog":
                    return new Dog(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3]);
                default:
                    throw new ArgumentException();
            }
        }

        public static Food CreateFood(string type, int foodQuantity)
        {
            switch (type)
            {
                case "Vegetable":
                    return new Vegetable(foodQuantity);
                case "Fruit":
                    return new Fruit(foodQuantity);
                case "Meat":
                    return new Meat(foodQuantity);
                case "Seeds":
                    return new Seeds(foodQuantity);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
