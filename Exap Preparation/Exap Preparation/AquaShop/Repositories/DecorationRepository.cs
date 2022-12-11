namespace AquaShop.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using AquaShop.Models.Decorations.Contracts;
    using AquaShop.Repositories.Contracts;

    public class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> models;

        public DecorationRepository()
        {
            models = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => models.AsReadOnly();

        public void Add(IDecoration model)
        {
            models.Add(model);
        }

        public IDecoration FindByType(string type)
        {
            return models.FirstOrDefault(x => x.GetType().Name == type);
        }

        public bool Remove(IDecoration model)
        {
            if (!models.Contains(model))
                return false;

            models.Remove(model);
            return true;
        }
    }
}
