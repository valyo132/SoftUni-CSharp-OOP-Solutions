namespace VehiclesExtension.Models
{
    public class Bus : BaseCar
    {
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        { }

        public override void Drive(double distance)
        {
            base.fuelConsumption += 1.4;
            base.Drive(distance);
            base.fuelConsumption -= 1.4;
        }

        public void DriveEmpty(double distance)
        {
            base.Drive(distance);
        }
    }
}
