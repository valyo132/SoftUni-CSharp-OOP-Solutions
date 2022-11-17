namespace Raiding.Models
{
    public class Rogue : BaseHero
    {
        public Rogue(string name, int power)
            : base(name, power)
        { }

        public override string CastAbility()
            => $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
    }
}
