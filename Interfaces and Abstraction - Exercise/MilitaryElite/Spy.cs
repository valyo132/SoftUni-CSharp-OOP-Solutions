namespace MilitaryElite
{
    using MilitaryElite.Interfaces;
    using System;

    public class Spy : Soldier, ISpy
    {
        public Spy(string id, string firstName, string lastName, int codeNumber) : base(id, firstName, lastName)
        {
            this.CodeNumber = codeNumber;
        }

        public int CodeNumber { get; set; }

        public override string ToString()
            => $"Name: {this.FirstName} {this.LastName} Id: {this.Id}" + Environment.NewLine +
                $"Code Number: {this.CodeNumber}";
    }
}
