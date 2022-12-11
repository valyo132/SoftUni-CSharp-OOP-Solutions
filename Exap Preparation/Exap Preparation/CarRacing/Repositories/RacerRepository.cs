namespace CarRacing.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.ExceptionServices;
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Repositories.Contracts;
    using CarRacing.Utilities.Messages;

    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> racers;

        public RacerRepository()
        {
            racers = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => racers.AsReadOnly();

        public void Add(IRacer model)
        {
            if (model == null)
                throw new ArgumentNullException(ExceptionMessages.InvalidAddRacerRepository);

            racers.Add(model);
        }

        public IRacer FindBy(string property)
            => racers.FirstOrDefault(x => x.Username == property);

        public bool Remove(IRacer model)
        {
            if (!racers.Contains(model))
                return false;

            racers.Remove(model);
            return false;
        }
    }
}
