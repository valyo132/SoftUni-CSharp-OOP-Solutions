namespace BirthdayCelebrations
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var list = new List<IBirthable>();

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string thing = tokens[0];
                string name = tokens[1];

                if (thing == "Citizen")
                {
                    int age = int.Parse(tokens[2]);
                    string id = tokens[3];
                    string birthDate = tokens[4];
                    IBirthable human = new Citizent(name, id, age, birthDate);
                    list.Add(human);
                }
                else if (thing == "Robot")
                {
                    string id = tokens[2];
                    IIdentifiable robot = new Robot(name, id);
                }
                else
                {
                    string birthDate = tokens[2];
                    IBirthable pet = new Pet(name, birthDate);
                    list.Add(pet);
                }
            }

            string dateToChek = Console.ReadLine();

            foreach (var item in list)
            {
                item.CheckBirthYear(dateToChek);
            }
        }
    }
}
