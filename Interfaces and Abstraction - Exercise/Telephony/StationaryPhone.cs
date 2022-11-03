namespace PersonInfo
{
    using System;
    using System.Linq;
    using Telephony.NewFolder;

    public class StationaryPhone : ICallable
    {
        public void Call(string number)
        {
            if (number.Any(x => !char.IsDigit(x)))
                Console.WriteLine("Invalid number!");
            else
                Console.WriteLine($"Dialing... {number}");
        }
    }
}
