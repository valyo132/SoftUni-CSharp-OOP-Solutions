namespace EasterRaces.Repositories
{
    using System.Linq;

    using EasterRaces.Models.Drivers.Contracts;
    using EasterRaces.Repositories.Entities;

    public class DriverRepository : Repository<IDriver>
    {
        public override IDriver GetByName(string name)
        {
            return Models.FirstOrDefault(x => x.Name == name);
        }
    }
}
