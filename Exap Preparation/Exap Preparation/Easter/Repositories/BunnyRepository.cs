namespace Easter.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Easter.Models.Bunnies.Contracts;
    using Easter.Repositories.Contracts;

    public class BunnyRepository : IRepository<IBunny>
    {
        private List<IBunny> models;

        public BunnyRepository()
        {
            models = new List<IBunny>(); 
        }

        public IReadOnlyCollection<IBunny> Models 
            => models.AsReadOnly();

        public void Add(IBunny model)
        {
            models.Add(model);
        }

        public IBunny FindByName(string name)
            => models.FirstOrDefault(x => x.Name == name);

        public bool Remove(IBunny model)
        {
            if (!models.Contains(model))
                return false;

            models.Remove(model);
            return true;
        }
    }
}
