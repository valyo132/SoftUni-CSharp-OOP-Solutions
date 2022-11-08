namespace SumofIntegers
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();

            int totalSum = 0;
            foreach (var item in input)
            {
                try
                {
                    int number = CheckNumber(item);

                    totalSum += number;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (OverflowException oe)
                {
                    Console.WriteLine(oe.Message);
                }

                Console.WriteLine($"Element '{item}' processed - current sum: {totalSum}");
            }

            Console.WriteLine($"The total sum of all integers is: {totalSum}");
        }

        private static int CheckNumber(string item)
        {
            long number = 0;
            bool isNumber = long.TryParse(item, out number);

            if (!isNumber)
            {
                throw new FormatException($"The element '{item}' is in wrong format!");
            }
            else
            {
                if (number < int.MinValue || number > int.MaxValue)
                    throw new OverflowException($"The element '{item}' is out of range!");
            }

            return Convert.ToInt32(number);
        }
    }
}
