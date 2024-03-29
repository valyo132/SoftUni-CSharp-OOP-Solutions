﻿namespace MilitaryElite
{
    using MilitaryElite.Interfaces;
    using System;

    public class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        public SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, string corps) : base(id, firstName, lastName, salary)
        {
            if (corps == "Airforces" || corps == "Marines")
                this.Corps = corps;
            else
                throw new ArgumentException();
        }

        public string Corps { get; set; }
    }
}
