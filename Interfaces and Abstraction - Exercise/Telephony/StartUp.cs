namespace PersonInfo
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            Smartphone smartphone = new Smartphone();
            StationaryPhone stationaryPhone = new StationaryPhone();

            string[] numbers = Console.ReadLine().Split(" ");
            string[] links = Console.ReadLine().Split(" ");

            foreach (var number in numbers)
            {
                if (number.Length == 10)
                    smartphone.Call(number);
                else if (number.Length == 7)
                    stationaryPhone.Call(number);
            }

            foreach (var link in links)
                smartphone.Browse(link);
        }
    }
}
