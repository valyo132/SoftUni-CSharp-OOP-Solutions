namespace Vehicles
{
    using System;
    using Vehicles.Models;

    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] carInput = Console.ReadLine().Split(); //

            double carFuelQuantity = double.Parse(carInput[1]);
            double carFuelConsumption = double.Parse(carInput[2]);
            BaseCar car = new Car(carFuelQuantity, carFuelConsumption);

            string[] truckInput = Console.ReadLine().Split(); //

            double truckFuelQuantity = double.Parse(truckInput[1]);
            double truckFuelConsumption = double.Parse(truckInput[2]);
            BaseCar truck = new Truck(truckFuelQuantity, truckFuelConsumption);

            int commandCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < commandCount; i++)
            {
                string[] command = Console.ReadLine().Split(); //
                string action = command[0];
                string vihecle = command[1];

                if (action == "Drive" && vihecle == "Car")
                {
                    double distance = double.Parse(command[2]);
                    car.Drive(distance);
                }
                else if (action == "Drive" && vihecle == "Truck")
                {
                    double distance = double.Parse(command[2]);
                    truck.Drive(distance);
                }
                else if (action == "Refuel" && vihecle == "Car")
                {
                    double fuel = double.Parse(command[2]);
                    car.Refuel(fuel);
                }
                else if (action == "Refuel" && vihecle == "Truck")
                {
                    double fuel = double.Parse(command[2]);
                    truck.Refuel(fuel);
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
