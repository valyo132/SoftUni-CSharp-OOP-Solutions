namespace SpaceStation.Models.Planets
{
    using System;
    using System.Collections.Generic;

    using SpaceStation.Models.Planets.Contracts;
    using SpaceStation.Utilities.Messages;

    public class Planet : IPlanet
    {
        private List<string> items;
        private string name;

        public Planet(string name)
        {
            this.Name = name;
            this.items = new List<string>();
        }

        public ICollection<string> Items => items;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }

                this.name = value;
            }
        }
    }
}
