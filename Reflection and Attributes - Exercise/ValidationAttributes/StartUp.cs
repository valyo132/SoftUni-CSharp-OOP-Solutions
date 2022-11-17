namespace ValidationAttributes
{
    using System;

    using Models;
    using Utilities;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person = new Person
             (
                 "Pesho",
                 15
             );

            bool isValidEntity = Validator.IsValid(person);

            Console.WriteLine(isValidEntity);
        }
    }
}
