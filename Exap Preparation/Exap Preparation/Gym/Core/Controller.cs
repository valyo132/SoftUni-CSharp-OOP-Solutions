namespace Gym.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Gym.Core.Contracts;
    using Gym.Models.Athletes;
    using Gym.Models.Athletes.Contracts;
    using Gym.Models.Equipment;
    using Gym.Models.Equipment.Contracts;
    using Gym.Models.Gyms;
    using Gym.Models.Gyms.Contracts;
    using Gym.Repositories;
    using Gym.Utilities.Messages;

    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym = gymType switch
            {
                nameof(BoxingGym) => new BoxingGym(gymName),
                nameof(WeightliftingGym) => new WeightliftingGym(gymName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidGymType)
            };

            gyms.Add(gym);

            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment item = equipmentType switch
            {
                nameof(BoxingGloves) => new BoxingGloves(),
                nameof(Kettlebell) => new Kettlebell(),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType)
            };

            equipment.Add(item);

            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            if (equipment.FindByType(equipmentType) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            var gym = gyms.FirstOrDefault(x => x.Name == gymName);
            var item = equipment.FindByType(equipmentType);

            gym.AddEquipment(item);
            equipment.Remove(item);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete person = athleteType switch
            {
                nameof(Boxer) => new Boxer(athleteName, motivation, numberOfMedals),
                nameof(Weightlifter) => new Weightlifter(athleteName, motivation, numberOfMedals),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType)
            };

            var gym = gyms.FirstOrDefault(x => x.Name == gymName);

            if ((gym is BoxingGym && person is Boxer) 
                || (gym is WeightliftingGym && person is Weightlifter))
            {
                gym.AddAthlete(person);

                return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
            }
            else
            {
                return OutputMessages.InappropriateGym;
            }
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            int count = gym.Athletes.Count;

            foreach (var person in gym.Athletes)
                person.Exercise();

            return string.Format(OutputMessages.AthleteExercise, count);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);

            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            foreach (var gym in gyms)
            {
                result.AppendLine(gym.GymInfo());
            }

            return result.ToString().TrimEnd();
        }
    }
}
