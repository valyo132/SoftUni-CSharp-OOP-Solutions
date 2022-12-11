namespace Gym.Models.Equipment
{
    using Gym.Models.Equipment.Contracts;

    public abstract class Equipment : IEquipment
    {
        private double weight;
        private decimal price;

        public Equipment(double weight, decimal prices)
        {
            this.Weight = weight;
            this.Price = price;
        }

        public double Weight
        {
            get => weight;
            private set => weight = value;
        }

        public decimal Price
        {
            get => price;
            private set => price = value;
        }
    }
}
