namespace EnterNumbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();

            while (list.Count < 10)
            {
                try
                {
                    if (list.Count == 0)
                        list.Add(ReadNumber(1, 100));
                    else
                        list.Add(ReadNumber(list.Max(), 100));
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            Console.WriteLine(String.Join(", ", list));
        }

        public static int ReadNumber(int start, int end)
        {
            string numberInput = Console.ReadLine();

            int number;
            bool isNumber = int.TryParse(numberInput, out number);

            if (!isNumber)
                throw new FormatException("Invalid Number!");

            if (number <= start || number >= end)
                throw new ArgumentException($"Your number is not in range {start} - {end}!");

            return number;
        }
    }
}
