namespace MilitaryElite
{
    using System.Collections.Generic;
    using System;
    using MilitaryElite.Interfaces;
    using System.Linq;
    using System.Text;

    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary, params Private[] privates) : base(id, firstName, lastName, salary)
        {
            this.Privates = new List<Private>(privates);
        }

        public List<Private> Privates { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.FirstName} {this.LastName} Id: {this.Id} Salary: {this.Salary:f2}");
            sb.AppendLine($"Privates:");
            if (this.Privates.Any())
                sb.Append(string.Join(Environment.NewLine, this.Privates.Select(x => $"  {x}")));
            return sb.ToString().TrimEnd();
        }
    }
}
