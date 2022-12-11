namespace Formula1.Core
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Formula1.Core.Contracts;
    using Formula1.Models;
    using Formula1.Models.Contracts;
    using Formula1.Models.FormulaOneCarTypes;
    using Formula1.Repositories;
    using Formula1.Repositories.Contracts;
    using Formula1.Utilities;

    public class Controller : IController
    {
        private IRepository<IPilot> pilotRepository;
        private IRepository<IRace> raceRepository;
        private IRepository<IFormulaOneCar> carRepository;

        public Controller()
        {
            this.pilotRepository = new PilotRepository();
            this.raceRepository = new RaceRepository();
            this.carRepository = new FormulaOneCarRepository();
        }
        public string CreatePilot(string fullName)
        {
            if (pilotRepository.FindByName(fullName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            var pilot = new Pilot(fullName);

            pilotRepository.Add(pilot);

            return String.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            FormulaOneCar car = type switch
            {
                nameof(Ferrari) => new Ferrari(model, horsepower, engineDisplacement),
                nameof(Williams) => new Williams(model, horsepower, engineDisplacement),
                _ => throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type))
            };

            carRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.FindByName(raceName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            var race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            if (pilotRepository.FindByName(pilotName) == null || pilotRepository.FindByName(pilotName).Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            if (carRepository.FindByName(carModel) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            var pilot = pilotRepository.FindByName(pilotName);
            var car = carRepository.FindByName(carModel);

            if (pilotRepository.Models.Any(x => x.Car == car))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {

            if (raceRepository.FindByName(raceName) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var race = raceRepository.FindByName(raceName);

            if (pilotRepository.FindByName(pilotFullName) == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            var pilot = pilotRepository.FindByName(pilotFullName);

            if (!pilot.CanRace || race.Pilots.Any(x => x.FullName == pilot.FullName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            if (raceRepository.FindByName(raceName) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var race = raceRepository.FindByName(raceName);

            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var fastestPilots = race.Pilots
                .OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps));

            IPilot winner = fastestPilots.First();
            IPilot second = fastestPilots.Skip(1).First();
            IPilot thirtd = fastestPilots.Skip(2).First();

            winner.WinRace();

            race.TookPlace = true;

            return $"Pilot {winner.FullName} wins the {raceName} race." + Environment.NewLine +
                $"Pilot {second.FullName} is second in the {raceName} race." + Environment.NewLine +
                $"Pilot {thirtd.FullName} is third in the {raceName} race.";
        }

        public string PilotReport()
        { 
            var pilots = pilotRepository.Models.OrderByDescending(x => x.NumberOfWins);

            return string.Join(Environment.NewLine, pilots);
        }

        public string RaceReport()
        {
            var races = raceRepository.Models.Where(x => x.TookPlace == true);

            return string.Join(Environment.NewLine, races.Select(x => x.RaceInfo()));
        }
    }
}
