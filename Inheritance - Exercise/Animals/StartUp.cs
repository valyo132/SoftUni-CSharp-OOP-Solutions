using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var list = new List<Animal>();

            string command;
            while ((command = Console.ReadLine()) != "Beast!")
            {
                string type = command;
                string[] animalInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = animalInfo[0];
                int age = int.Parse(animalInfo[1]);
                if (age < 0)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                string gender = "";
                if (type != "Tomcat" && type != "Kitten")
                    gender = animalInfo[2];

                if (type == "Cat")
                {
                    Cat animal = new Cat(name, age, gender);
                    list.Add(animal);
                }
                else if (type == "Dog")
                {
                    Dog animal = new Dog(name, age, gender);
                    list.Add(animal);
                }
                else if (type == "Frog")
                {
                    Frog animal = new Frog(name, age, gender);
                    list.Add(animal);
                }
                else if (type == "Tomcat")
                {
                    Tomcat animal = new Tomcat(name, age);
                    list.Add(animal);
                }
                else
                {
                    Kitten animal = new Kitten(name, age);
                    list.Add(animal);
                }
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
                item.ProduceSound();
            }
        }
    }
}
