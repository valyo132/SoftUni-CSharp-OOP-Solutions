﻿namespace EasterRaces.Models.Races.Entities
{
    using EasterRaces.Models.Drivers.Contracts;
    using EasterRaces.Models.Races.Contracts;
    using EasterRaces.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Race : IRace
    {
        private string name;
        private int laps;
        private List<IDriver> drivers;

        public Race(string name, int laps)
        {
            this.Name = name;
            this.Laps = laps;
            this.drivers = new List<IDriver>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }

                this.name = value;
            }
        }

        public int Laps
        {
            get { return this.laps; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, 1));
                }

                this.laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => this.drivers.AsReadOnly();

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
                throw new ArgumentNullException(ExceptionMessages.DriverInvalid);

            if (driver.CanParticipate == false)
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));

            if (this.drivers.Contains(driver))
                throw new ArgumentException(string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name));

            this.drivers.Add(driver);
        }
    }
}
