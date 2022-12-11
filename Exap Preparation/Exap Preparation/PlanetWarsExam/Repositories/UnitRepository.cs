namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Repositories.Contracts;

    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> models;

        public UnitRepository()
        {
            models = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => models.AsReadOnly();

        public void AddItem(IMilitaryUnit model)
        {
            models.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
            => models.FirstOrDefault(x => x.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            var item = models.FirstOrDefault(y => y.GetType().Name == name);

            return models.Remove(item);
        }
    }
}
