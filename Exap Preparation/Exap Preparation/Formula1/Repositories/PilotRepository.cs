namespace Formula1.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Formula1.Models.Contracts;
    using Formula1.Repositories.Contracts;

    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;

        public PilotRepository()
        {
            this.pilots = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models => this.pilots;

        public void Add(IPilot model)
        {
            this.pilots.Add(model);
        }

        public IPilot FindByName(string name)
        {
            return this.pilots.FirstOrDefault(x => x.FullName == name);
        }

        public bool Remove(IPilot model)
        {
            if (!this.pilots.Contains(model))
                return false;

            this.pilots.Remove(model);
            return true;
        }
    }
}
