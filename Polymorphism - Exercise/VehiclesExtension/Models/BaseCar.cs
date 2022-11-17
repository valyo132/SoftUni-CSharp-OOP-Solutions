namespace VehiclesExtension.Models
{
    public abstract class BaseCar
    {
        protected double fuelQuantity;
        protected double fuelConsumption;
        protected double tankCapacity;

        public BaseCar(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.fuelConsumption = fuelConsumption;
            this.tankCapacity = tankCapacity;

            if (fuelQuantity <= tankCapacity)
                this.fuelQuantity = fuelQuantity;
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
            if (liters > 0)
            {
                if (this.fuelQuantity + liters > this.tankCapacity)
                    System.Console.WriteLine($"Cannot fit {liters} fuel in the tank");
                else
                    this.fuelQuantity += liters;
            }
            else
            {
                System.Console.WriteLine("Fuel must be a positive number");
            }
        }

        public override string ToString()
            => $"{this.GetType().Name}: {this.fuelQuantity:f2}";
    }
}
