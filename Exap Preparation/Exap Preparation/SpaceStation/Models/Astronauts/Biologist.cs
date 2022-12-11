namespace SpaceStation.Models.Astronauts
{
    using System;

    public class Biologist : Astronaut
    {
        public Biologist(string name)
            : base(name, 70)
        { }

        public override void Breath()
        {
            this.Oxygen = Math.Max(this.Oxygen - 5, 0);
        }
    }
}
