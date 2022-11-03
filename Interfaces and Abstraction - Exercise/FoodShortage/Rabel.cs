namespace FoodShortage
{
    public class Rabel : IBuyer
    {
        public Rabel(string name, int age, string group)
        {
            this.Name = name;
            this.Age = age;
            Group = group;
            this.Food = 0;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
        public int Food { get; set; }

        public int BuyFood()
        {
            Food += 5;
            return 5;
        }
    }
}
