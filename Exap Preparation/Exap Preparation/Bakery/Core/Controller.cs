namespace Bakery.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Bakery.Core.Contracts;
    using Bakery.Models.BakedFoods;
    using Bakery.Models.BakedFoods.Contracts;
    using Bakery.Models.Drinks;
    using Bakery.Models.Drinks.Contracts;
    using Bakery.Models.Tables;
    using Bakery.Models.Tables.Contracts;
    using Bakery.Utilities.Messages;

    public class Controller : IController
    {
        private decimal income;
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;

        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink = type switch
            {
                nameof(Tea) => new Tea(name, portion, brand),
                nameof(Water) => new Water(name, portion, brand),
                _ => null
            };

            drinks.Add(drink);

            return string.Format(OutputMessages.DrinkAdded, name, brand);
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food = type switch
            {
                nameof(Bread) => new Bread(name, price),
                nameof(Cake) => new Cake(name, price),
                _ => null
            };

            bakedFoods.Add(food);

            return string.Format(OutputMessages.FoodAdded, name, type);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = type switch
            {
                nameof(InsideTable) => new InsideTable(tableNumber, capacity),
                nameof(OutsideTable) => new OutsideTable(tableNumber, capacity),
                _ => null
            };

            tables.Add(table);

            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder result = new StringBuilder();

            var collection = tables.Where(x => x.IsReserved == false).ToList();

            foreach (var table in collection)
            {
                result.AppendLine(table.GetFreeTableInfo());
            }

            return result.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {income:f2}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            StringBuilder result = new StringBuilder();

            var table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            var bill = table.GetBill();
            table.Clear();

            result.AppendLine($"Table: {tableNumber}");
            result.AppendLine($"Bill: {bill:f2}");

            income += bill;

            return result.ToString().TrimEnd();
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            var drink = drinks.FirstOrDefault(x => x.Name == drinkName && x.Brand == drinkBrand);

            if (table == null)
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);

            if (drink == null)
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);

            table.OrderDrink(drink);

            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            var food = bakedFoods.FirstOrDefault(x => x.Name == foodName);

            if (table == null)
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);

            if (food == null)
                return string.Format(OutputMessages.NonExistentFood, foodName);

            table.OrderFood(food);

            return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string ReserveTable(int numberOfPeople)
        {
            var table = tables.FirstOrDefault(x => x.Capacity >= numberOfPeople && x.IsReserved == false);

            if (table == null)
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);

            table.Reserve(numberOfPeople);

            return string.Format(OutputMessages.TableReserved, table.TableNumber, numberOfPeople);
        }
    }
}
