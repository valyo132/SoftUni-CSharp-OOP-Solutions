namespace FootballTeamGenerator
{
    using System;

    public class Player
    {
        private string name;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Name = name;
            this.Stats = new Stats(endurance, sprint, dribble, passing, shooting);
        }

        public Stats Stats { get; private set; }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A name should not be empty.");
                this.name = value;
            }
        }

        public double Skill
            => (this.Stats.Endurance + this.Stats.Sprint + this.Stats.Dribble + this.Stats.Passing + this.Stats.Shooting) / 5.0;
    }
}
