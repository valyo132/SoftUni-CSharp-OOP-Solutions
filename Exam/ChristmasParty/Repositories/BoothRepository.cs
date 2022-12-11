﻿namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using ChristmasPastryShop.Models.Booths.Contracts;
    using ChristmasPastryShop.Repositories.Contracts;

    public class BoothRepository : IRepository<IBooth>
    {
        private List<IBooth> models;

        public BoothRepository()
        {
            models = new List<IBooth>();
        }

        public IReadOnlyCollection<IBooth> Models => models.AsReadOnly();

        public void AddModel(IBooth model)
        {
            models.Add(model);
        }
    }
}
