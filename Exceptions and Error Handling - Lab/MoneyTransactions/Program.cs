namespace MoneyTransactions
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] bankAccountsInput = Console.ReadLine().Split(',');

            Dictionary<int, double> bankAccounts = GetBankAccounts(bankAccountsInput);

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                try
                {
                    ExecuteCommands(bankAccounts, command);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }
            }
        }

        private static void ExecuteCommands(Dictionary<int, double> bankAccounts, string command)
        {
            string[] tokens = command.Split();
            string action = GetCommand(tokens);
            int accNumber = GetAccountNumber(tokens, bankAccounts);
            double sum = double.Parse(tokens[2]);

            if (action == "Deposit")
            {
                bankAccounts[accNumber] += sum;
            }
            else if (action == "Withdraw")
            {
                if (IsSumValid(sum, bankAccounts, accNumber))
                {
                    bankAccounts[accNumber] -= sum;
                }
            }

            Console.WriteLine($"Account {accNumber} has new balance: {bankAccounts[accNumber]:f2}");
        }

        private static bool IsSumValid(double sum, Dictionary<int, double> bankAccounts, int accNumber)
        {
            if (bankAccounts[accNumber] < sum)
                throw new ArgumentException("Insufficient balance!");

            return true;
        }

        private static int GetAccountNumber(string[] tokens, Dictionary<int, double> bankAccounts)
        {
            int number = int.Parse(tokens[1]);

            if (!bankAccounts.ContainsKey(number))
                throw new ArgumentException("Invalid account!");

            return number;
        }

        private static string GetCommand(string[] tokens)
        {
            string command = tokens[0];

            if (command != "Deposit" && command != "Withdraw")
                throw new FormatException("Invalid command!");

            return command;
        }

        private static Dictionary<int, double> GetBankAccounts(string[] bankAccountsInput)
        {
            var bankAccounts = new Dictionary<int, double>();

            foreach (var item in bankAccountsInput)
            {
                string[] tokens = item.Split('-'); //
                int accNumber = int.Parse(tokens[0]);
                double accBalance = double.Parse(tokens[1]);

                bankAccounts.Add(accNumber, accBalance);
            }

            return bankAccounts;
        }
    }
}
