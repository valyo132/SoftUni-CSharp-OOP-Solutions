namespace ChristmasPastryShop.Models.Booths
{
    using System;
    using System.Linq;
    using System.Text;

    using ChristmasPastryShop.Models.Booths.Contracts;
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Repositories;
    using ChristmasPastryShop.Repositories.Contracts;
    using ChristmasPastryShop.Utilities.Messages;

    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            this.delicacyMenu = new DelicacyRepository();
            this.cocktailMenu = new CocktailRepository();
            CurrentBill = 0;
            Turnover = 0;
            IsReserved = false;
        }

        public int BoothId
        {
            get { return boothId; }
            private set { boothId = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }

                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyMenu;
        public IRepository<ICocktail> CocktailMenu => cocktailMenu;

        public double CurrentBill
        {
            get { return currentBill; }
            private set { currentBill = value; }
        }

        public double Turnover
        {
            get { return turnover; }
            private set { turnover = value; }
        }

        public bool IsReserved
        {
            get { return isReserved; }
            private set { isReserved = value; }
        }

        public void ChangeStatus()
        {
            this.IsReserved = !IsReserved;
        }

        public void Charge()
        {
            this.Turnover = this.CurrentBill;
            this.CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.CurrentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Booth: {this.BoothId}");
            result.AppendLine($"Capacity: {this.Capacity}");
            result.AppendLine($"Turnover: {this.Turnover:f2} lv");
            result.AppendLine($"-Cocktail menu:");
            if (CocktailMenu.Models.Any())
                result.AppendLine($"{string.Join(Environment.NewLine, this.CocktailMenu.Models)}");
            result.AppendLine($"-Delicacy menu:");
            if (DelicacyMenu.Models.Any())
                result.AppendLine($"{string.Join(Environment.NewLine, this.DelicacyMenu.Models)}");

            return result.ToString().TrimEnd();
        }
    }
}
