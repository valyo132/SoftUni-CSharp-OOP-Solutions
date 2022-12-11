namespace OnlineShop.Models
{
    using System;

    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Components;

    public abstract class Component : Product, IComponent
    {
        private int generation;

        public Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.Generation = generation;
        }

        public int Generation
        {
            get { return generation; }
            private set { generation = value; }
        }

        public override string ToString()
          => base.ToString() + String.Format(SuccessMessages.ComponentToString, generation);
    }
}
