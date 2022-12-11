namespace EasterRaces.Models.Cars.Entities
{
    using System;

    using EasterRaces.Models.Cars.Contracts;
    using EasterRaces.Utilities.Messages;

    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private double cubicCentimeters;
        private int minHorsePower;
        private int maxHorsePower;

        public Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.Model = model;
            if (horsePower < minHorsePower || horsePower > maxHorsePower)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, horsePower));
            }
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
        }

        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, 4));
                }

                this.model = value;
            }
        }

        public int HorsePower
        {
            get { return horsePower; }
            private set
            {
                this.horsePower = value;
            }
        }


        public double CubicCentimeters
        {
            get { return cubicCentimeters; }
            private set { cubicCentimeters = value; }
        }

        public double CalculateRacePoints(int laps)
        {
            return CubicCentimeters / horsePower * laps;
        }
    }
}
