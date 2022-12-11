namespace PlanetWars.Models.Planets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Models.MilitaryUnits;
    using Models.MilitaryUnits.Contracts;
    using Models.Planets.Contracts;
    using Models.Weapons;
    using Models.Weapons.Contracts;
    using Utilities.Messages;

    public class Planet : IPlanet
    {
        private string name;
        private double buget;
        private List<IMilitaryUnit> army;
        private List<IWeapon> wepons;

        public Planet(string name, double buget)
        {
            this.Name = name;
            this.Budget = buget;
            army = new List<IMilitaryUnit>();
            wepons = new List<IWeapon>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }

        public double Budget
        {
            get { return buget; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }

                buget = value;
            }
        }

        public double MilitaryPower 
            => CalculateTotalMilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => army.AsReadOnly();

        public IReadOnlyCollection<IWeapon> Weapons => wepons.AsReadOnly();

        public void AddUnit(IMilitaryUnit unit)
        {
            army.Add(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            wepons.Add(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder reslt = new StringBuilder();

            string forces = army.Any() ? string.Join(", ", army.Select(x => x.GetType().Name)) : "No units";
            string weponsInfo = wepons.Any() ? string.Join(", ", wepons.Select(x => x.GetType().Name)) : "No weapons";

            reslt.AppendLine($"Planet: {this.Name}");
            reslt.AppendLine($"--Budget: {this.Budget} billion QUID");
            reslt.AppendLine($"--Forces: {forces}");
            reslt.AppendLine($"--Combat equipment: {weponsInfo}");
            reslt.AppendLine($"--Military Power: {this.MilitaryPower}");

            return reslt.ToString().TrimEnd();
        }

        public void Profit(double amount)
            => this.Budget += amount;

        public void Spend(double amount)
        {
            if (amount > this.Budget)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var force in army)
                force.IncreaseEndurance();
        }

        private double CalculateTotalMilitaryPower()
        {
            double value = army.Sum(x => x.EnduranceLevel) + wepons.Sum(x => x.DestructionLevel);

            if (army.Any(x => x.GetType() == typeof(AnonymousImpactUnit)))
            {
                value += value * 0.3;
            }

            if (wepons.Any(x => x.GetType() == typeof(NuclearWeapon)))
            {
                value += value * 0.45;
            }

            return Math.Round(value, 3);
        }
    }
}
