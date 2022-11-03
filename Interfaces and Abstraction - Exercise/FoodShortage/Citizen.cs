namespace FoodShortage
{
    public class Citizen : IBuyer
    {
        public Citizen(string name, int age, string id, string bitrthDate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.BirthDate = bitrthDate;
            this.Food = 0;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string BirthDate { get; set; }
        public int Food { get; set; }

        public int BuyFood()
        {
            Food += 10;
            return 10;
        }
    }
}
