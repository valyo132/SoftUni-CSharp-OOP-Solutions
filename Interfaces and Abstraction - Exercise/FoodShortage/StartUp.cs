namespace FoodShortage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var peopleList = new List<IBuyer>();

            int countOfPeople = int.Parse(Console.ReadLine());

            for (int i = 0; i < countOfPeople; i++)
            {
                string[] personInfo = Console.ReadLine().Split(" ");
                string name = personInfo[0];
                int age = int.Parse(personInfo[1]);

                if (personInfo.Length == 3)
                {
                    string group = personInfo[2];
                    IBuyer citizen = new Rabel(name, age, group);
                    peopleList.Add(citizen);
                }
                else
                {
                    string id = personInfo[2];
                    string birthDate = personInfo[3];
                    IBuyer human = new Citizen(name, age, id, birthDate);
                    peopleList.Add(human);
                }
            }

            int totalFood = 0;

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                if (peopleList.Any(x => x.Name == command))
                {
                    var person = peopleList.First(x => x.Name == command);
                    totalFood += person.BuyFood();
                }
            }

            Console.WriteLine(totalFood);
        }
    }
}
