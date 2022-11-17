namespace WildFarm.Models.AnimalClasses
{
    public class Cat : Feline
    {
        public Cat(string name, double weight, int foodEaten, string livingRegion, string breed) : base(name, weight, foodEaten, livingRegion, breed)
        { }

        public override string Talk()
            => "Meow";
    }
}
