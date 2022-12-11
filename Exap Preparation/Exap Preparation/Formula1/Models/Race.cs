namespace Formula1.Models
{
    using System;
    using System.Collections.Generic;

    using Formula1.Models.Contracts;
    using Formula1.Utilities;

    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private List<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            this.RaceName = raceName;
            this.NumberOfLaps = numberOfLaps;
            this.pilots = new List<IPilot>();
            TookPlace = false;
        }

        public string RaceName
        {
            get { return raceName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }

                raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get { return numberOfLaps; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }

                numberOfLaps = value;
            }
        }

        public bool TookPlace { get; set; }

        public ICollection<IPilot> Pilots => this.pilots;

        public void AddPilot(IPilot pilot)
        {
            Pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            string tookPlace = TookPlace ? "Yes" : "No";

            return $"The {RaceName} race has:" + Environment.NewLine +
                $"Participants: {Pilots.Count}" + Environment.NewLine +
                $"Number of laps: {NumberOfLaps}" + Environment.NewLine +
                $"Took place: {tookPlace}";
        }
    }
}
