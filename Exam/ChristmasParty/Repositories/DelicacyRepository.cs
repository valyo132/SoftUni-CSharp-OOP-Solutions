namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Repositories.Contracts;

    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private List<IDelicacy> models;

        public DelicacyRepository()
        {
            models = new List<IDelicacy>();
        }

        public IReadOnlyCollection<IDelicacy> Models => models.AsReadOnly();

        public void AddModel(IDelicacy model)
        {
            models.Add(model);
        }
    }
}
