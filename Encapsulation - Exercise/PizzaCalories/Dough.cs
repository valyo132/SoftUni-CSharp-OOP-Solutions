using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Dough
    {
        private Dictionary<string, double> typeOfDough = new Dictionary<string, double>()
        {
            ["white"] = 1.5,
            ["wholegrain"] = 1.0
        };

        private Dictionary<string, double> typeOfbakingTech = new Dictionary<string, double>()
        {
            ["crispy"] = 0.9,
            ["chewy"] = 1.1,
            ["homemade"] = 1.0
        };

        private double doughModifire;
        private double bakingModifire;

        private string type;
        private string bakingTechnique;
        private double weigh;

        public Dough(string type, string bakingTechnique, double weigh)
        {
            Type = type;
            BakingTechnique = bakingTechnique;
            Weigh = weigh;
        }

        public double Weigh
        {
            get { return weigh; }
            private set
            {
                if (value < 1 || value > 200)
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                weigh = value;
            }
        }

        public string BakingTechnique
        {
            get { return bakingTechnique; }
            private set
            {
                if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
                    throw new ArgumentException("Invalid type of dough.");
                bakingTechnique = value;

                bakingModifire = typeOfbakingTech[value.ToLower()];
            }
        }

        public string Type
        {
            get { return type; }
            private set
            {
                if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                    throw new ArgumentException("Invalid type of dough.");
                type = value;

                doughModifire = typeOfDough[value.ToLower()];
            }
        }

        public double TotalCalories()
            => (2 * Weigh) * doughModifire * bakingModifire;
    }
}
