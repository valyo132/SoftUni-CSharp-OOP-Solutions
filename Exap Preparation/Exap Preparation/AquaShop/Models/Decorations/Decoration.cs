namespace AquaShop.Models.Decorations
{
    using AquaShop.Models.Decorations.Contracts;

    public abstract class Decoration : IDecoration
    {
        private int comfort;
        private decimal price;

        public Decoration(int comfort, decimal price)
        {
            this.Comfort = comfort;
            this.Price = price;
        }

        public int Comfort
        {
            get => comfort;
            private set => comfort = value;
        }

        public decimal Price
        {
            get => price;
            private set => price = value;
        }
    }
}
