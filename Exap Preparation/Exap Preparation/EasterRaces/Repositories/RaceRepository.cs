namespace EasterRaces.Repositories
{
    using System.Linq;

    using EasterRaces.Models.Races.Contracts;
    using EasterRaces.Repositories.Entities;

    public class RaceRepository : Repository<IRace>
    {
        public override IRace GetByName(string name)
        {
            return Models.FirstOrDefault(x => x.Name == name);
        }
    }
}
