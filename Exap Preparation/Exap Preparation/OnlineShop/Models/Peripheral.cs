namespace OnlineShop.Models
{
    using System;

    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Peripherals;

    public abstract class Peripheral : Product, IPeripheral
    {
        public Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.ConnectionType = connectionType;
        }

        public string ConnectionType { get; private set; }

        public override string ToString()
        => base.ToString() + String.Format(SuccessMessages.PeripheralToString, ConnectionType);
    }
}
