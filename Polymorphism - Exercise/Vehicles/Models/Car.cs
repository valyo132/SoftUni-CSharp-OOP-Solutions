namespace Vehicles.Models
{
    internal class Car : BaseCar
    {
        public Car(double fuelQuantity, double fuelConsumption)
            : base(fuelQuantity, fuelConsumption + 0.9)
        { }
    }
}
