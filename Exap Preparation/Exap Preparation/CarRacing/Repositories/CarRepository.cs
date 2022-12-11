namespace CarRacing.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Repositories.Contracts;
    using CarRacing.Utilities.Messages;

    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> cars;

        public CarRepository()
        {
            cars = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models => cars.AsReadOnly();

        public void Add(ICar model)
        {
            if (model == null)
                throw new ArgumentNullException(ExceptionMessages.InvalidAddCarRepository);

            cars.Add(model);
        }

        public ICar FindBy(string property)
            => cars.FirstOrDefault(x => x.VIN == property);

        public bool Remove(ICar model)
        {
            if (!cars.Contains(model))
                return false;

            cars.Remove(model);
            return true;
        }
    }
}
