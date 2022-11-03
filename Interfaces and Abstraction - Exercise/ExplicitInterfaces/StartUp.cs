namespace ExplicitInterfaces
{
    using ExplicitInterfaces.Interfaces;
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string name = tokens[0];
                string country = tokens[1];
                int age = int.Parse(tokens[2]);

                IResident resident = new Citizen(name, country, age);
                IPerson person = new Citizen(name, country, age);

                Console.WriteLine(person.GetName());
                Console.WriteLine(resident.GetName());
            }
        }
    }
}
