namespace SquareRoot
{
    using System;

    internal class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                int number = int.Parse(Console.ReadLine());
                if (number < 0)
                    throw new FormatException("Invalid number.");

                Console.WriteLine(Math.Sqrt(number));
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }

            Console.WriteLine("Goodbye.");
        }
    }
}
