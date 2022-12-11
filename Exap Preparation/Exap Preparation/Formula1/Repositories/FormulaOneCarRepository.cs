namespace Formula1.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Formula1.Models.Contracts;
    using Formula1.Repositories.Contracts;

    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly List<IFormulaOneCar> models;

        public FormulaOneCarRepository()
        {
            this.models = new List<IFormulaOneCar>();
        }

        public IReadOnlyCollection<IFormulaOneCar> Models => this.models;

        public void Add(IFormulaOneCar model)
        {
            this.models.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            return this.models.FirstOrDefault(x => x.Model == name);
        }

        public bool Remove(IFormulaOneCar model)
        {
            if (!this.models.Contains(model))
                return false;

            this.models.Remove(model);
            return true;
        }
    }
}
