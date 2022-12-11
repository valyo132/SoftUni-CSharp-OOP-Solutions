namespace PlanetWars.Models.MilitaryUnits
{
    using System;

    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Utilities.Messages;

    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        public MilitaryUnit(double cost)
        {
            this.Cost = cost;
            this.EnduranceLevel = 1;
        }

        public double Cost
        {
            get { return cost; }
            private set { cost = value; }
        }

        public int EnduranceLevel
        {
            get { return enduranceLevel; }
            private set { enduranceLevel = value; }
        }

        public void IncreaseEndurance()
        {
            this.EnduranceLevel++;

            if (this.EnduranceLevel > 20)
            {
                this.EnduranceLevel = 20;

                throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
            }
        }
    }
}
