
namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Repositories.Contracts;

    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> models;

        public WeaponRepository()
        {
            models = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => models.AsReadOnly();

        public void AddItem(IWeapon model)
        {
            models.Add(model);
        }

        public IWeapon FindByName(string name)
            => models.FirstOrDefault(x => x.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            var item = models.FirstOrDefault(y => y.GetType().Name == name);

            return models.Remove(item);
        }
    }
}
