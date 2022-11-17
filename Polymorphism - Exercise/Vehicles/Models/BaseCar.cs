namespace Vehicles.Models
{
    public abstract class BaseCar
    {
        private double fuelQuantity;
        private double fuelConsumption;

        public BaseCar(double fuelQuantity, double fuelConsumption)
        {
            this.fuelQuantity = fuelQuantity;
            this.fuelConsumption = fuelConsumption;
        }

        public virtual void Drive(double distance)
        {
            double neededFuel = this.fuelConsumption * distance;

            if (this.fuelQuantity - neededFuel >= 0)
            {
                this.fuelQuantity -= neededFuel;
                System.Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
            }
            else
            {
                System.Console.WriteLine($"{this.GetType().Name} needs refueling");
            }
        }

        public virtual void Refuel(double liters)
        {
            this.fuelQuantity += liters;
        }

        public override string ToString()
            => $"{this.GetType().Name}: {this.fuelQuantity:f2}";
    }
}
