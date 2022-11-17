namespace WildFarm.Models.AnimalClasses
{
    public abstract class Animal
    {
        public Animal(string name, double weight, int foodEaten)
        {
            this.Name = name;
            this.Weight = weight;
            this.FoodEaten = foodEaten;
        }

        public abstract string Talk();

        public string Name { get; set; }
        public double Weight { get; set; }
        public int FoodEaten { get; set; }
    }
}
