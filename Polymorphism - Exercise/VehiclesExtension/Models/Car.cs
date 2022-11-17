namespace VehiclesExtension.Models
{
    internal class Car : BaseCar
    {
        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + 0.9, tankCapacity)
        { }
    }
}
