namespace ChristmasPastryShop.Core
{
    using System.Linq;
    using System.Text;

    using ChristmasPastryShop.Core.Contracts;
    using ChristmasPastryShop.Models.Booths;
    using ChristmasPastryShop.Models.Booths.Contracts;
    using ChristmasPastryShop.Models.Cocktails;
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Models.Delicacies;
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Repositories;
    using ChristmasPastryShop.Utilities.Messages;

    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int id = booths.Models.Count + 1;

            var booth = new Booth(id, capacity);
            booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded, id, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != nameof(Stolen) && delicacyTypeName != nameof(Gingerbread))
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.Any(x => x.DelicacyMenu.Models.Any(x => x.Name == delicacyName)))
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy item = delicacyTypeName switch
            {
                nameof(Stolen) => new Stolen(delicacyName),
                nameof(Gingerbread) => new Gingerbread(delicacyName),
                _ => null
            };

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            booth.DelicacyMenu.AddModel(item);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (booths.Models.Any(x => x.CocktailMenu.Models.Any(x => x.Name == cocktailName && x.Size == size)))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            ICocktail cocktail = cocktailTypeName switch
            {
                nameof(Hibernation) => new Hibernation(cocktailName, size),
                nameof(MulledWine) => new MulledWine(cocktailName, size),
                _ => null
            };

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            booth.CocktailMenu.AddModel(cocktail);

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            IBooth booth = booths.Models
                .OrderBy(x => x.Capacity)
                .ThenByDescending(x => x.BoothId)
                .FirstOrDefault(x => !x.IsReserved && x.Capacity >= countOfPeople);

            if (booth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();

            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] tokens = order.Split("/");
            string itemTypeName = tokens[0];
            string itemName = tokens[1];
            int countOfOrders = int.Parse(tokens[2]);
            string size = "";
            if (tokens.Length == 4)
            {
                size = tokens[3];
            }

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (itemTypeName != nameof(Hibernation) && itemTypeName != nameof(MulledWine)
                && itemTypeName != nameof(Gingerbread) && itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            if (!booth.CocktailMenu.Models.Any(x => x.Name == itemName)
                && !booth.DelicacyMenu.Models.Any(x => x.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            if (tokens.Length == 4)
            {
                ICocktail item = booth.CocktailMenu.Models.FirstOrDefault(x => x.Name == itemName && x.GetType().Name == itemTypeName && x.Size == size);

                if (item == null)
                {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }

                booth.UpdateCurrentBill(item.Price * countOfOrders);

                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrders, itemName);
            }
            else
            {
                IDelicacy item = booth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == itemName && x.GetType().Name == itemTypeName);

                if (item == null)
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, size, itemName);
                }

                booth.UpdateCurrentBill(item.Price * countOfOrders);

                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrders, itemName);
            }
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            booth.Charge();
            booth.ChangeStatus();

            StringBuilder result = new StringBuilder();

            result.AppendLine($"Bill {booth.Turnover:f2} lv");
            result.AppendLine($"Booth {boothId} is now available!");

            return result.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            return booth.ToString();
        }
    }
}
