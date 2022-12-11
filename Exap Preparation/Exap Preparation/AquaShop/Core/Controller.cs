namespace AquaShop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Core.Contracts;
    using Models.Aquariums;
    using Models.Aquariums.Contracts;
    using Models.Decorations;
    using Models.Decorations.Contracts;
    using Models.Fish;
    using Models.Fish.Contracts;
    using Repositories;
    using Utilities.Messages;

    public class Controller : IController
    {
        private DecorationRepository decorations;
        private List<IAquarium> aquariums;

        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium = aquariumType switch
            {
                nameof(FreshwaterAquarium) => new FreshwaterAquarium(aquariumName),
                nameof(SaltwaterAquarium) => new SaltwaterAquarium(aquariumName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType)
            };

            aquariums.Add(aquarium);

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = decorationType switch
            {
                nameof(Ornament) => new Ornament(),
                nameof(Plant) => new Plant(),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType)
            };

            decorations.Add(decoration);

            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = fishType switch
            {
                nameof(FreshwaterFish) => new FreshwaterFish(fishName, fishSpecies, price),
                nameof(SaltwaterFish) => new SaltwaterFish(fishName, fishSpecies, price),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidFishType)
            };

            var aquarium = aquariums.Find(x => x.Name == aquariumName);

            if (fish.GetType().Name == nameof(FreshwaterFish))
            {
                if (aquarium.GetType().Name == nameof(FreshwaterAquarium))
                    aquarium.AddFish(fish);
                else
                    return OutputMessages.UnsuitableWater;
            }
            else if (fish.GetType().Name == nameof(SaltwaterFish))
            {
                if (aquarium.GetType().Name == nameof(SaltwaterAquarium))
                    aquarium.AddFish(fish);
                else
                    return OutputMessages.UnsuitableWater;
            }

            return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string CalculateValue(string aquariumName)
        {
            var aqurium = aquariums.Find(x => x.Name == aquariumName);

            var value = aqurium.Fish.Sum(x => x.Price) + aqurium.Decorations.Sum(x => x.Price);

            return $"The value of Aquarium {aquariumName} is {value:f2}.";
        }

        public string FeedFish(string aquariumName)
        {
            var aqurium = aquariums.Find(x => x.Name == aquariumName);

            aqurium.Feed();

            return $"Fish fed: {aqurium.Fish.Count}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            if (decorations.FindByType(decorationType) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }

            var decoration = decorations.FindByType(decorationType);
            var aquarium = aquariums.Find(x => x.Name == aquariumName);

            aquarium.AddDecoration(decoration);
            decorations.Remove(decoration);

            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            foreach (var aqua in aquariums)
            {
                result.AppendLine(aqua.GetInfo());
            }

            return result.ToString().TrimEnd();
        }
    }
}
