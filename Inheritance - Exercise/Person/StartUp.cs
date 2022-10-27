using System;

namespace Person
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            Person person;
            if (age > 15)
                person = new Person(name, age);
            else
                person = new Child(name, age);

            Console.WriteLine(person);
        }
    }
}