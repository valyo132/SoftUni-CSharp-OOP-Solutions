namespace Raiding
{
    using System;
    using System.Collections.Generic;
    using Raiding.Models;

    internal class StartUp
    {
        static void Main(string[] args)
        {
            var allHeroes = new List<BaseHero>();

            int heroCount = int.Parse(Console.ReadLine());

            while (allHeroes.Count < heroCount)
            {
                string name = Console.ReadLine();
                string type = Console.ReadLine();

                try
                {
                    BaseHero hero = HeroFactory.CrateHero(type, name, 0);
                    allHeroes.Add(hero);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            int bossHealth = int.Parse(Console.ReadLine());

            int totalHeroDamage = 0;
            foreach (var item in allHeroes)
            {
                Console.WriteLine(item.CastAbility());
                totalHeroDamage += item.Power;
            }

            if (totalHeroDamage >= bossHealth)
                Console.WriteLine("Victory!");
            else
                Console.WriteLine("Defeat...");
        }
    }
}
