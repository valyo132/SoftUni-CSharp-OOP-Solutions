namespace PlanetWars.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Core.Contracts;
    using Models.MilitaryUnits;
    using Models.MilitaryUnits.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Models.Weapons;
    using Models.Weapons.Contracts;
    using Repositories;
    using Utilities.Messages;

    public class Controller : IController
    {
        private PlanetRepository planets;

        private IPlanet winnerPlanet;
        private IPlanet looserPlanet;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            var planet = planets.FindByName(planetName);

            IMilitaryUnit unit = unitTypeName switch
            {
                nameof(SpaceForces) => new SpaceForces(),
                nameof(AnonymousImpactUnit) => new AnonymousImpactUnit(),
                nameof(StormTroopers) => new StormTroopers(),
                _ => throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName))
            };

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            planet.AddUnit(unit);
            planet.Spend(unit.Cost);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            var planet = planets.FindByName(planetName);

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon = weaponTypeName switch
            {
                nameof(BioChemicalWeapon) => new BioChemicalWeapon(destructionLevel),
                nameof(NuclearWeapon) => new NuclearWeapon(destructionLevel),
                nameof(SpaceMissiles) => new SpaceMissiles(destructionLevel),
                _ => throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName, planetName))
            };

            planet.AddWeapon(weapon);
            planet.Spend(weapon.Price);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            var planet = new Planet(name, budget);
            planets.AddItem(planet);

            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
            {
                result.AppendLine(planet.PlanetInfo());
            }

            return result.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var first = planets.FindByName(planetOne);
            var second = planets.FindByName(planetTwo);

            bool firstPlanetNuclear = first.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));
            bool secondPlanetNuclear = second.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));

            if (first.MilitaryPower > second.MilitaryPower)
            {
                TakeAWinner(first, second);
            }
            else if (first.MilitaryPower < second.MilitaryPower)
            {
                TakeAWinner(second, first);
            }
            else
            {
                if (firstPlanetNuclear && secondPlanetNuclear)
                {
                    first.Spend(first.Budget / 2);
                    second.Spend(second.Budget / 2);

                    return OutputMessages.NoWinner;
                }
                else if (firstPlanetNuclear)
                {
                    TakeAWinner(first, second);
                }
                else if (firstPlanetNuclear)
                {
                    TakeAWinner(second, first);
                }
                else
                {
                    first.Spend(first.Budget / 2);
                    second.Spend(second.Budget / 2);

                    return OutputMessages.NoWinner;
                }
            }

            return string.Format(OutputMessages.WinnigTheWar, winnerPlanet.Name, looserPlanet.Name);
        }

        public string SpecializeForces(string planetName)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            var planet = planets.FindByName(planetName);

            if (!planet.Army.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

            planet.TrainArmy();
            planet.Spend(1.25);

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        private void TakeAWinner(IPlanet winner, IPlanet looser)
        {
            winner.Spend(winner.Budget / 2);
            winner.Profit(looser.Budget / 2);

            var valueToAdd = looser.Army.Sum(x => x.Cost) + looser.Weapons.Sum(x => x.Price);
            winner.Profit(valueToAdd);

            planets.RemoveItem(looser.Name);

            winnerPlanet = winner;
            looserPlanet = looser;
        }
    }
}
