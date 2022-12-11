namespace ChristmasPastryShop.Models.Cocktails
{
    using System;

    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Utilities.Messages;

    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        public Cocktail(string name, string size, double price)
        {
            Name = name;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }

                name = value;
            }
        }

        public string Size
        {
            get { return size; }
            private set { size = value; }
        }

        public double Price
        {
            get { return price; }
            private set
            {
                if (this.Size == "Large")
                    price = value;
                else if (this.Size == "Middle")
                    price = 0.6666666666 * value;
                else if(this.Size == "Small")
                    price = 0.3333333333 * value;
            }
        }

        public override string ToString()
        {
            return $"--{this.Name} ({this.Size}) - {this.Price:f2} lv";
        }
    }
}
