namespace WildFarm.Models.AnimalClasses
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, int foodEaten, double wingSize) 
            : base(name, weight, foodEaten, wingSize)
        { }

        public override string Talk()
            => "Cluck";
    }
}
