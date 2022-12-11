namespace Easter.Models.Dyes
{
    using System;

    using Easter.Models.Dyes.Contracts;

    public class Dye : IDye
    {
        private int power;

        public Dye(int power)
        {
            this.Power = power;
        }

        public int Power
        {
            get { return power; }
            private set
            {
                if (value < 0)
                    power = 0;
                else
                    power = value;
            }
        }

        public bool IsFinished()
            => this.Power == 0;

        public void Use()
        {
            this.Power -= 10;

            if (Power < 0)
                this.Power = 0;
        }
    }
}
