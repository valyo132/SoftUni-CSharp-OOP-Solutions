namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        public Druid(string name, int power) 
            : base(name, power)
        { }

        public override string CastAbility()
            => $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
    }
}
