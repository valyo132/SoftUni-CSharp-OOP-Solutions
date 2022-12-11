namespace Easter.Models.Bunnies
{
    using System;
    using System.Collections.Generic;

    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Dyes.Contracts;
    using Easter.Utilities.Messages;

    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private List<IDye> dyes;

        public Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            this.dyes = new List<IDye>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }

                this.name = value;
            }
        }

        public int Energy
        {
            get { return this.energy; }
            protected set
            {
                if (value < 0)
                    this.energy = 0;
                else
                    this.energy = value;
            }
        }

        public ICollection<IDye> Dyes => dyes.AsReadOnly();

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public abstract void Work();
    }
}
