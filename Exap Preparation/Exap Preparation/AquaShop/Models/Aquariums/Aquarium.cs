namespace AquaShop.Models.Aquariums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AquaShop.Models.Aquariums.Contracts;
    using AquaShop.Models.Decorations.Contracts;
    using AquaShop.Models.Fish.Contracts;
    using AquaShop.Utilities.Messages;

    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private List<IDecoration> decorations;
        private List<IFish> fish;

        public Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            decorations = new List<IDecoration>();
            fish = new List<IFish>();
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this.name = value;
            }
        }

        public int Capacity
        {
            get => this.capacity;
            private set => this.capacity = value;
        }

        public int Comfort 
            => (int)decorations.Sum(x => x.Comfort);

        public ICollection<IDecoration> Decorations => decorations.AsReadOnly();

        public ICollection<IFish> Fish => fish.AsReadOnly();

        public void AddDecoration(IDecoration decoration)
        {
            decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (this.fish.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }

        public void Feed()
        {
            this.fish.ForEach(x => x.Eat());
        }

        public string GetInfo()
        {
            //if (!this.fish.Any())
            //{
            //    return "Fish: none";
            //}

            string fishInfo = fish.Any() ? string.Join(", ", fish.Select(x => x.Name)) : "none";

            return $"{this.Name} ({GetType().Name}):" + Environment.NewLine +
                $"Fish: {fishInfo}" + Environment.NewLine +
                $"Decorations: {decorations.Count}" + Environment.NewLine +
                $"Comfort: {this.Comfort}";
        }

        public bool RemoveFish(IFish fish)
        {
            if (fish == null)
                return false;

            this.fish.Remove(fish);
            return true;
        }
    }
}
