namespace Heroes.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Core.Contracts;
    using Models.Contracts;
    using Models.Heroes;
    using Models.Map;
    using Models.Weapons;
    using Repositories;

    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository wepons;

        public Controller()
        {
            heroes = new HeroRepository();
            wepons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (heroes.FindByName(heroName) == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }

            if (wepons.FindByName(weaponName) == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }

            var hero = heroes.FindByName(heroName);
            var wepon = wepons.FindByName(weaponName);

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }

            hero.AddWeapon(wepon);
            wepons.Remove(wepon);

            return $"Hero {heroName} can participate in battle using a {wepon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }

            IHero hero = type switch
            {
                nameof(Knight) => new Knight(name, health, armour),
                nameof(Barbarian) => new Barbarian(name, health, armour),
                _ => throw new InvalidOperationException("Invalid hero type.")
            };

            heroes.Add(hero);

            if (hero.GetType().Name == nameof(Knight))
                return $"Successfully added Sir {name} to the collection.";

            return $"Successfully added Barbarian {name} to the collection.";
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (wepons.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }

            IWeapon wepon = type switch
            {
                nameof(Mace) => new Mace(name, durability),
                nameof(Claymore) => new Claymore(name, durability),
                _ => throw new InvalidOperationException("Invalid weapon type.")
            };

            wepons.Add(wepon);

            return $"A {wepon.GetType().Name.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            StringBuilder result = new StringBuilder();

            foreach (var hero in heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name))
            {
                string weponsInfo = hero.Weapon == null ? "Unarmed" : hero.Weapon.Name;

                result.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                result.AppendLine($"--Health: { hero.Health }");
                result.AppendLine($"--Armour: { hero.Armour }");
                result.AppendLine($"--Weapon: {weponsInfo}");
            }

            return result.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            Map map = new Map();

            var collection = heroes.Models
                .Where(x => x.IsAlive && x.Weapon != null)
                .ToList();

            string result = map.Fight(collection);

            return result;
        }
    }
}
