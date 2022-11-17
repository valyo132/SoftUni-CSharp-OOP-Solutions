namespace VehiclesExtension.Models
{
    public class Truck : BaseCar
    {
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + 1.6, tankCapacity)
        { }

        public override void Refuel(double liters)
        {
            if (liters <= 0)
                System.Console.WriteLine("Fuel must be a positive number");
            else if (this.fuelQuantity + liters * 0.95 > this.tankCapacity)
                System.Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            else
                base.Refuel(liters * 0.95);
        }
    }
}
