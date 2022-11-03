namespace MilitaryElite
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using MilitaryElite.Interfaces;
    using System.Text;

    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(string id, string firstName, string lastName, decimal salary, string corps, params string[] missions) : base(id, firstName, lastName, salary, corps)
        {
            this.Missions = new List<Mission>();
            for (int i = 0; i < missions.Length; i += 2)
            {
                string name = missions[i];
                string state = missions[i + 1];
                if (state == "inProgress" || state == "Finished")
                    this.Missions.Add(new Mission(name, state));
            }
        }

        public List<Mission> Missions { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.FirstName} {this.LastName} Id: {this.Id} Salary: {this.Salary:f2}");
            sb.AppendLine($"Corps: {this.Corps}");
            sb.AppendLine("Missions:");
            if (this.Missions.Any())
                sb.Append(String.Join(Environment.NewLine, this.Missions.Select(x => $"  {x}")));
            return sb.ToString().TrimEnd();
        }
    }
}
