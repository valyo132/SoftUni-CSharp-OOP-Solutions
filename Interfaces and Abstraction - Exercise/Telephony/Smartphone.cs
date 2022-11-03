namespace PersonInfo
{
    using System;
    using System.Linq;
    using Telephony.NewFolder;

    public class Smartphone : ICallable, ISmartphonealbe
    {
        public void Browse(string link)
        {
            if (link.Any(x => char.IsDigit(x)))
                Console.WriteLine("Invalid URL!");
            else
                Console.WriteLine($"Browsing: {link}!");
        }

        public void Call(string number)
        {
            if (number.Any(x => !char.IsDigit(x)))
                Console.WriteLine("Invalid number!");
            else
                Console.WriteLine($"Calling... {number}");
        }
    }
}
