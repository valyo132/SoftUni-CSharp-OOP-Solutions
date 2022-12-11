namespace NavalVessels.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NavalVessels.Core.Contracts;
    using NavalVessels.Models;
    using NavalVessels.Models.Contracts;
    using NavalVessels.Models.VesselTypes;
    using NavalVessels.Repositories;
    using NavalVessels.Utilities.Messages;

    public class Controller : IController
    {
        private VesselRepository vessels;
        private List<ICaptain> captians;

        public Controller()
        {
            vessels = new VesselRepository();
            captians = new List<ICaptain>();
        }

        public string HireCaptain(string fullName)
        {
            ICaptain captain = new Captain(fullName);

            if (captians.Any(x => x.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            captians.Add(captain);

            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (vessels.FindByName(name) != null)
                return String.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            if (vesselType != "Submarine" && vesselType != "Battleship")
                return OutputMessages.InvalidVesselType;

            IVessel vesselToProduce;
            if (vesselType == "Submarine")
                vesselToProduce = new Submarine(name, mainWeaponCaliber, speed);
            else
                vesselToProduce = new Battleship(name, mainWeaponCaliber, speed);

            vessels.Add(vesselToProduce);
            return String.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var currCapatin = captians.Find(x => x.FullName == selectedCaptainName);
            var currVessel = vessels.FindByName(selectedVesselName);

            if (currCapatin == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            if (currVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            if (currVessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }

            currCapatin.AddVessel(currVessel);
            currVessel.Captain = currCapatin;
            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain currCapatin = captians.Find(x => x.FullName == captainFullName);

            return currCapatin.Report();
        }

        public string VesselReport(string vesselName)
        {
            IVessel currVessel = vessels.FindByName(vesselName);
            return currVessel.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var currVessel = vessels.FindByName(vesselName);

            if (currVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }

            if (currVessel.GetType().Name == "Battleship")
            {
                (currVessel as Battleship).ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
            else
            {
                (currVessel as Submarine).ToggleSubmergeMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
        }

        public string ServiceVessel(string vesselName)
        {
            var currVessel = vessels.FindByName(vesselName);

            if (currVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }

            currVessel.RepairVessel();

            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var first = vessels.FindByName(attackingVesselName);
            var second = vessels.FindByName(defendingVesselName);

            if (first == null)
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);

            if (second == null)
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);

            if (first.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);

            if (second.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);

            first.Attack(second);

            first.Captain.IncreaseCombatExperience();
            second.Captain.IncreaseCombatExperience();

            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, second.ArmorThickness);
        }
    }
}
