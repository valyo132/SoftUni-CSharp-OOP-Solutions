namespace NavalVessels.Models.VesselTypes
{
    using System;

    public class Submarine : Vessel
    {
        private const int SUBMARINE_INITIAL_ARMORR_THIKNESS = 200;

        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, SUBMARINE_INITIAL_ARMORR_THIKNESS)
        {
            this.SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;
            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }
        public override void RepairVessel()
        {
            if (ArmorThickness < SUBMARINE_INITIAL_ARMORR_THIKNESS)
                ArmorThickness = SUBMARINE_INITIAL_ARMORR_THIKNESS;
        }

        public override string ToString()
        {
            string mode = this.SubmergeMode ? "ON" : "OFF";

            return base.ToString() + Environment.NewLine + $" *Submerge mode: {mode}";
        }
    }
}
