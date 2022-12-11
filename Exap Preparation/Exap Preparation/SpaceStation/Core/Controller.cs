namespace SpaceStation.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Core.Contracts;
    using Models.Astronauts;
    using Models.Astronauts.Contracts;
    using Models.Mission;
    using Models.Planets;
    using Repositories;
    using Utilities.Messages;

    public class Controller : IController
    {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private Mission mission;
        private int planetsExplored = 0;

        public Controller()
        {
            astronauts = new AstronautRepository();
            planets = new PlanetRepository();
            mission = new Mission();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = type switch
            {
                nameof(Biologist) => new Biologist(astronautName),
                nameof(Geodesist) => new Geodesist(astronautName),
                nameof(Meteorologist) => new Meteorologist(astronautName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType)
            };

            astronauts.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);

            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planets.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var valid = astronauts.Models.Where(x => x.Oxygen > 60).ToList();
            var planet = planets.FindByName(planetName);

            if (!valid.Any())
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);

            mission.Explore(planet, valid);
            planetsExplored++;

            int deadPeople = valid.Where(x => !x.CanBreath).Count();

            return string.Format(OutputMessages.PlanetExplored, planetName, deadPeople);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{planetsExplored} planets were explored!");
            result.AppendLine($"Astronauts info:");

            foreach (var astronaut in astronauts.Models)
            {
                string items = astronaut.Bag.Items.Any() ? string.Join(", ", astronaut.Bag.Items) : "none";

                result.AppendLine($"Name: {astronaut.Name}");
                result.AppendLine($"Oxygen: {astronaut.Oxygen}");
                result.AppendLine($"Bag items: {items}");
            }

            return result.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            if (astronauts.FindByName(astronautName) == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));

            var person = astronauts.FindByName(astronautName);

            astronauts.Remove(person);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
