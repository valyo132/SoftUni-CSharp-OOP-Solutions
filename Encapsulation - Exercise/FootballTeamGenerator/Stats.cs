namespace FootballTeamGenerator
{
    using System;

    public class Stats
    {
        private const string OUT_OF_RANGE_STAT_EXCEPTION = "{0} should be between 0 and 100.";

        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Stats(int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
        }

        public int Shooting
        {
            get { return this.shooting; }
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(String.Format(OUT_OF_RANGE_STAT_EXCEPTION, nameof(Shooting)));
                this.shooting = value;
            }
        }

        public int Passing
        {
            get { return this.passing; }
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(String.Format(OUT_OF_RANGE_STAT_EXCEPTION, nameof(Passing)));
                this.passing = value;
            }
        }

        public int Dribble
        {
            get { return this.dribble; }
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(String.Format(OUT_OF_RANGE_STAT_EXCEPTION, nameof(Dribble)));
                this.dribble = value;
            }
        }

        public int Sprint
        {
            get { return this.sprint; }
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(String.Format(OUT_OF_RANGE_STAT_EXCEPTION, nameof(Sprint)));
                this.sprint = value;
            }
        }

        public int Endurance
        {
            get { return this.endurance; }
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(String.Format(OUT_OF_RANGE_STAT_EXCEPTION, nameof(Endurance)));
                this.endurance = value;
            }
        }
    }
}
