using Formula1.Models;

namespace Formula1.Models.FormulaOneCarTypes
{
    public class Ferrari : FormulaOneCar
    {
        public Ferrari(string model, int horsepower, double engineDisplacement)
            : base(model, horsepower, engineDisplacement)
        { }
    }
}
