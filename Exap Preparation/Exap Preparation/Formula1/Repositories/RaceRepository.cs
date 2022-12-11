namespace Formula1.Repositories
{
    using System.Linq;
    using System.Collections.Generic;

    using Formula1.Models.Contracts;
    using Formula1.Repositories.Contracts;

    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => this.races;

        public void Add(IRace model)
        {
            this.races.Add(model);
        }

        public IRace FindByName(string name)
        {
            return this.races.FirstOrDefault(x => x.RaceName == name);
        }

        public bool Remove(IRace model)
        {
            if (!this.races.Contains(model))
                return false;

            this.races.Remove(model);
            return true;
        }
    }
}
