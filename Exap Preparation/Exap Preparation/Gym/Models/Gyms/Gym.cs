namespace Gym.Models.Gyms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using global::Gym.Models.Athletes.Contracts;
    using global::Gym.Models.Equipment.Contracts;
    using global::Gym.Models.Gyms.Contracts;
    using global::Gym.Utilities.Messages;

    public abstract class Gym : IGym
    {
        private string name;
        private int capacity;
        private List<IEquipment> equipment;
        private List<IAthlete> athletes;

        public Gym(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.equipment = new List<IEquipment>();
            this.athletes = new List<IAthlete>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }

                this.name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public double EquipmentWeight 
            => this.Equipment.Sum(x => x.Weight);

        public ICollection<Equipment.Contracts.IEquipment> Equipment => equipment;

        public ICollection<Athletes.Contracts.IAthlete> Athletes => athletes;

        public void AddAthlete(Athletes.Contracts.IAthlete athlete)
        {
            if (this.Athletes.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }

            this.Athletes.Add(athlete);
        }

        public void AddEquipment(Equipment.Contracts.IEquipment equipment)
        {
            this.Equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var person in Athletes)
            {
                person.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder result = new StringBuilder();

            string athletesInfo = this.Athletes.Any() ? string.Join(", ", this.Athletes.Select(x => x.FullName)) : "No athletes";

            result.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            result.AppendLine($"Athletes: {athletesInfo}");
            result.AppendLine($"Equipment total count: {this.Equipment.Count}");
            result.AppendLine($"Equipment total weight: {this.EquipmentWeight:f2} grams");

            return result.ToString().TrimEnd();
        }

        public bool RemoveAthlete(Athletes.Contracts.IAthlete athlete)
            => this.Athletes.Remove(athlete);
    }
}
