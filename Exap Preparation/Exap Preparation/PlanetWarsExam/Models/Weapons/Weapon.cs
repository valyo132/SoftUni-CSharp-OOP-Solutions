namespace PlanetWars.Models.Weapons
{
    using System;

    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Utilities.Messages;

    public abstract class Weapon : IWeapon
    {
        private double price;
        private int destructionLevel;

        public Weapon(int destructionLevel, double price)
        {
            this.DestructionLevel = destructionLevel;
            this.Price = price;
        }

        public double Price
        {
            get { return price; }
            private set { price = value; }
        }

        public int DestructionLevel
        {
            get { return destructionLevel; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);
                }

                if (value > 10)
                {
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);
                }

                this.destructionLevel = value;
            }
        }
    }
}
