namespace Cards
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        static void Main(string[] args)
        {
            var cards = new List<Card>();

            string[] cardsInput = Console.ReadLine().Split(", "); //

            foreach (var item in cardsInput)
            {
                string[] tokens = item.Split(); //
                string face = tokens[0];
                string suit = tokens[1];

                try
                {
                    if (IsFaceValid(face) && IsSuitValid(suit))
                    {
                        Card card = new Card(face, suit);
                        cards.Add(card);
                    }
                    else
                    {
                        throw new FormatException("Invalid card!");
                    }

                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            Console.WriteLine(string.Join(" ", cards));
        }

        public class Card
        {
            private Dictionary<string, string> cardsSuits = new Dictionary<string, string>()
            {
                ["S"] = "\u2660",
                ["H"] = "\u2665",
                ["D"] = "\u2666",
                ["C"] = "\u2663"
            };

            public Card(string face, string suit)
            {
                this.Face = face;
                this.Suit = suit;
            }

            public string Face { get; }
            public string Suit { get; }

            public override string ToString()
                => $"[{this.Face}{cardsSuits[this.Suit]}]";
        }

        private static bool IsFaceValid(string face)
        {
            if (face != "2" && face != "3" && face != "4" && face != "5" && face != "6" && face != "7" && face != "8" &&
                face != "9" && face != "10" && face != "J" && face != "Q" && face != "K" && face != "A")
                return false;
            else
                return true;
        }

        private static bool IsSuitValid(string suit)
        {
            if (suit != "S" && suit != "H" && suit != "D" && suit != "C")
                return false;
            else
                return true;
        }

    }
}
