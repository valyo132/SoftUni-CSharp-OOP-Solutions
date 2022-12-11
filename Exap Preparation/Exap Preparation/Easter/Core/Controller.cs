namespace Easter.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Easter.Core.Contracts;
    using Easter.Models.Bunnies;
    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Dyes;
    using Easter.Models.Eggs;
    using Easter.Models.Workshops;
    using Easter.Repositories;
    using Easter.Utilities.Messages;

    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;
        private Workshop workshop;

        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
            workshop = new Workshop();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = bunnyType switch
            {
                nameof(HappyBunny) => new HappyBunny(bunnyName),
                nameof(SleepyBunny) => new SleepyBunny(bunnyName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType)
            };

            bunnies.Add(bunny);

            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            if (bunnies.FindByName(bunnyName) == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            var bunny = bunnies.FindByName(bunnyName);
            var dye = new Dye(power);

            bunny.AddDye(dye);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var egg = eggs.FindByName(eggName);

            var validBunnies = bunnies.Models
                .OrderByDescending(x => x.Energy)
                .TakeWhile(x => x.Energy >= 50);

            if (!validBunnies.Any())
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);

            foreach (var bunny in validBunnies)
            {
                workshop.Color(egg, bunny);

                if (bunny.Energy == 0)
                    bunnies.Remove(bunny);
            }

            if (egg.IsDone())
                return string.Format(OutputMessages.EggIsDone, eggName);

            return string.Format(OutputMessages.EggIsNotDone, eggName);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            var coloredEggs = eggs.Models.Where(x => x.IsDone()).Count();

            result.AppendLine($"{coloredEggs} eggs are done!");
            result.AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                result.AppendLine($"Name: {bunny.Name}");
                result.AppendLine($"Energy: {bunny.Energy}");
                result.AppendLine($"Dyes: {bunny.Dyes.Where(x => !x.IsFinished()).Count()} not finished");
            }

            return result.ToString().TrimEnd();
        }
    }
}
