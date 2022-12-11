namespace Easter.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Easter.Models.Eggs.Contracts;
    using Easter.Repositories.Contracts;

    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> models;

        public EggRepository()
        {
            models = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models 
            => models.AsReadOnly();

        public void Add(IEgg model)
        {
            models.Add(model);
        }

        public IEgg FindByName(string name)
            => models.FirstOrDefault(x => x.Name == name);

        public bool Remove(IEgg model)
        {
            if (!models.Contains(model))
                return false;

            models.Remove(model);
            return true;
        }
    }
}
