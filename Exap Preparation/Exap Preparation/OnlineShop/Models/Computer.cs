namespace OnlineShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Components;
    using OnlineShop.Models.Products.Computers;
    using OnlineShop.Models.Products.Peripherals;

    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
        {
            get
            {
                if (!components.Any())
                    return base.OverallPerformance;

                return base.OverallPerformance + this.components.Average(x => x.OverallPerformance);
            }
        }

        public override decimal Price
             => base.Price + components.Sum(x => x.Price) + peripherals.Sum(x => x.Price);

        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals.AsReadOnly();

        public void AddComponent(IComponent component)
        {
            if (this.components.Any(x => x.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, this.Id));
            }

            this.components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.peripherals.Any(x => x.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }

            this.peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (!this.components.Any(x => x.GetType().Name == componentType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            var compoment = this.components.Find(x => x.GetType().Name == componentType);
            this.components.Remove(compoment);

            return compoment;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (!this.peripherals.Any(x => x.GetType().Name == peripheralType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }

            var peripherial = this.peripherals.Find(x => x.GetType().Name == peripheralType);
            this.peripherals.Remove(peripherial);

            return peripherial;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count}):");
            if (components.Any())
                sb.AppendLine(String.Join(Environment.NewLine, components.Select(x => $"  {x}")));

            double averagePerformance = peripherals.Count > 0 ? peripherals.Average(x => x.OverallPerformance) : 0;
            sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({averagePerformance:f2}):");
            if (peripherals.Any())
                sb.Append(String.Join(Environment.NewLine, peripherals.Select(x => $"  {x}")));

            return sb.ToString().TrimEnd();
        }
    }
}
