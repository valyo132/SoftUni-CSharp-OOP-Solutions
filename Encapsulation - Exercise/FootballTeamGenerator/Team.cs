namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
    {
        private string name;
        private List<Player> players;

        private Team()
        {
            this.players = new List<Player>();
        }

        public Team(string name)
            : this()
        {
            this.Name = name;
        }

        public int Rating
            => players.Count > 0 ?(int)Math.Round(players.Average(x => x.Skill), 0) : 0;
        

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A name should not be empty.");
                name = value;
            }
        }

        public void AddPlayer(Player player)
            => players.Add(player);

        public void RemovePlayer(string playerName)
        {
            Player player = players.FirstOrDefault(x => x.Name == playerName);
            if (player == null)
                throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");

            players.Remove(player);
        }

        public override string ToString()
            => $"{this.Name} - {this.Rating}";
    }
}
