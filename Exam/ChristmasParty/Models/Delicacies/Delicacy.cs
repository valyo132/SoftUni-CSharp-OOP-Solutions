namespace ChristmasPastryShop.Models.Delicacies
{
    using System;

    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Utilities.Messages;

    public abstract class Delicacy : IDelicacy
    {
        private string name;
        private double price;

        public Delicacy(string name, double price)
        {
            this.Name = name;
            this.Price = price;
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

        public double Price
        {
            get { return price; }
            private set { price = value; }
        }

        public override string ToString()
        {
            return $"--{this.Name} - {this.Price:f2} lv";
        }
    }
}
