namespace NavalVessels.Models
{
    using System;

    public class Battleship : Vessel
    {
        private const double INITIAL_ARMOR_THICKNESS = 300;

        public bool SonarMode { get; private set; }

        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, INITIAL_ARMOR_THICKNESS)
        {
            SonarMode = false;
        }

        public void ToggleSonarMode()
        {
            SonarMode = !SonarMode;
            if (SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
        }

        public override void RepairVessel()
        {
            if (ArmorThickness < INITIAL_ARMOR_THICKNESS)
                ArmorThickness = INITIAL_ARMOR_THICKNESS;
        }

        public override string ToString()
        {
            string sonarModeText = SonarMode ? "ON" : "OFF";
            return base.ToString() + Environment.NewLine + $" *Sonar mode: {sonarModeText}";
        }
    }
}