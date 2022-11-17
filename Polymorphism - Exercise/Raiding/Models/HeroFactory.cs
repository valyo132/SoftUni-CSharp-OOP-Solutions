namespace Raiding.Models
{
    using System;

    public class HeroFactory
    {
        private const int DRUID_POWER = 80;
        private const int PALADIN_POWER = 100;
        private const int ROGUE_POWER = 80;
        private const int WARRIOR_POWER = 100;

        public static BaseHero CrateHero(string type, string name, int power)
        {
            switch (type)
            {
                case "Druid":
                    return new Druid(name, DRUID_POWER);
                case "Paladin":
                    return new Paladin(name, PALADIN_POWER);
                case "Rogue":
                    return new Rogue(name, ROGUE_POWER);
                case "Warrior":
                    return new Warrior(name, WARRIOR_POWER);
                default:
                    throw new ArgumentException("Invalid hero!");
            }
        }
    }
}
