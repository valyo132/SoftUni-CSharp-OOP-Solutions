namespace Bakery.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Bakery.Models.BakedFoods.Contracts;
    using Bakery.Models.Drinks.Contracts;
    using Bakery.Models.Tables.Contracts;
    using Bakery.Utilities.Messages;

    public abstract class Table : ITable
    {
        private int tableNumber;
        private int capacity;
        private int numberOfPeople;
        private decimal pricePerPerson;
        private bool isReserved;
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinkOrders;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
            this.foodOrders = new List<IBakedFood>();
            this.drinkOrders = new List<IDrink>();
        }

        public int TableNumber
        {
            get { return tableNumber; }
            private set { tableNumber = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                this.capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson
        {
            get { return pricePerPerson; }
            private set { pricePerPerson = value; }
        }

        public bool IsReserved
        {
            get { return isReserved; }
            private set { isReserved = value; }
        }

        public decimal Price
            => this.NumberOfPeople * this.PricePerPerson;

        public void Clear()
        {
            foodOrders.Clear();
            drinkOrders.Clear();

            this.IsReserved = false;
            this.NumberOfPeople = 0;
        }

        public decimal GetBill()
        {
            return foodOrders.Sum(x => x.Price) + drinkOrders.Sum(x => x.Price) + this.Price;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Table: {this.TableNumber}");
            result.AppendLine($"Type: {this.GetType().Name}");
            result.AppendLine($"Capacity: {this.Capacity}");
            result.AppendLine($"Price per Person: {this.PricePerPerson}");

            return result.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            this.IsReserved = true;
            this.NumberOfPeople = numberOfPeople;
        }
    }
}
