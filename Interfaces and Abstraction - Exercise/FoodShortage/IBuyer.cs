namespace FoodShortage
{
    public interface IBuyer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Food { get; set; }

        public int BuyFood();
    }
}
