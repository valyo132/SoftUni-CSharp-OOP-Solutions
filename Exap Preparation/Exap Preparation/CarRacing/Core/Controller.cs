namespace CarRacing.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using CarRacing.Core.Contracts;
    using CarRacing.Models.Cars;
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Models.Maps;
    using CarRacing.Models.Racers;
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Repositories;
    using CarRacing.Utilities.Messages;

    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private Map map;

        public Controller()
        {
            cars = new CarRepository();
            racers = new RacerRepository();
            map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car = type switch
            {
                nameof(SuperCar) => new SuperCar(make, model, VIN, horsePower),
                nameof(TunedCar) => new TunedCar(make, model, VIN, horsePower),
                _ => throw new ArgumentException(ExceptionMessages.InvalidCarType)
            };

            cars.Add(car);

            return string.Format(OutputMessages.SuccessfullyAddedCar, car.Make, car.Model, car.VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            if (cars.FindBy(carVIN) == null)
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);

            var car = cars.FindBy(carVIN);

            IRacer racer = type switch
            {
                nameof(ProfessionalRacer) => new ProfessionalRacer(username, car),
                nameof(StreetRacer) => new StreetRacer(username, car),
                _ => throw new ArgumentException(ExceptionMessages.InvalidRacerType)
            };

            racers.Add(racer);

            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            if (racers.FindBy(racerOneUsername) == null)
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));

            if (racers.FindBy(racerTwoUsername) == null)
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));

            var recerOne = racers.FindBy(racerOneUsername);
            var recerTwo = racers.FindBy(racerTwoUsername);

            string result = map.StartRace(recerOne, recerTwo);

            return result;
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            foreach (var racer in racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(x => x.Username))
            {
                result.AppendLine($"{racer.GetType().Name}: {racer.Username}");
                result.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                result.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                result.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }

            // Check if each races is on e new separate line.
            return result.ToString().TrimEnd();
        }
    }
}
