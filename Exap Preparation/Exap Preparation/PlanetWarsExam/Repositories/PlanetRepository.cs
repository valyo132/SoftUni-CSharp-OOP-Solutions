namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using PlanetWars.Models.Planets.Contracts;
    using PlanetWars.Repositories.Contracts;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> models;

        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => models.AsReadOnly();

        public void AddItem(IPlanet model)
        {
            models.Add(model);
        }

        public IPlanet FindByName(string name)
            => models.FirstOrDefault(x => x.Name == name);

        public bool RemoveItem(string name)
        {
            var item = models.FirstOrDefault(x => x.Name == name);

            return models.Remove(item);
        }
    }
}
