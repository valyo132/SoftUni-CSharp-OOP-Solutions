namespace WildFarm.Models.AnimalClasses
{
    public class Dog : Mammal
    {
        public Dog(string name, double weight, int foodEaten, string livingRegion)
            : base(name, weight, foodEaten, livingRegion) { }

        public override string Talk()
            => "Woof!";
    }
}
