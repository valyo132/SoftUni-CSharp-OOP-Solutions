namespace PlayCatch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> inputList = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            int thrownExceprions = 0;
            while (thrownExceprions < 3)
            {
                try
                {
                    ExecuteCommands(inputList);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                    thrownExceprions++;
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                    thrownExceprions++;
                }
            }

            Console.WriteLine(String.Join(", ", inputList));
        }

        private static void ExecuteCommands(List<int> inputList)
        {
            string[] command = Console.ReadLine().Split();
            string action = command[0];

            if (action == "Replace")
            {
                int index = GetNumber(command[1]);
                int element = GetNumber(command[2]);

                if (IsNumberValid(index, inputList))
                {
                    inputList[index] = element;
                }
            }
            else if (action == "Print")
            {
                int startindex = GetNumber(command[1]);
                int endIndex = GetNumber(command[2]);

                if (IsNumberValid(startindex, inputList) && IsNumberValid(endIndex, inputList))
                {
                    List<int> listToPrint = new List<int>();

                    for (int i = startindex; i <= endIndex; i++)
                    {
                        listToPrint.Add(inputList[i]);
                    }

                    Console.WriteLine(String.Join(", ", listToPrint));
                }
            }
            else if (action == "Show")
            {
                int index = GetNumber(command[1]);

                if (IsNumberValid(index, inputList))
                    Console.WriteLine(inputList[index]);
            }
        }

        private static bool IsNumberValid(int element, List<int> inputList)
        {
            if (element >= 0 && element < inputList.Count)
                return true;

            throw new ArgumentException("The index does not exist!");
        }

        private static int GetNumber(string item)
        {
            int number = 0;
            bool isNumber = int.TryParse(item, out number);

            if (!isNumber)
                throw new FormatException("The variable is not in the correct format!");

            return number;
        }
    }
}
