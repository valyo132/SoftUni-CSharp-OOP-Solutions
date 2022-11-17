namespace Raiding.Models
{
    public class Paladin : BaseHero
    {
        public Paladin(string name, int power)
            : base(name, power)
        { }

        public override string CastAbility()
            => $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
    }
}
