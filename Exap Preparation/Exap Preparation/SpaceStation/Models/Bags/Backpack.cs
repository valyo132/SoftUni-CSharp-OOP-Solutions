namespace SpaceStation.Models.Bags
{
    using System.Collections.Generic;

    using SpaceStation.Models.Bags.Contracts;

    public class Backpack : IBag
    {
        private List<string> items;

        public Backpack()
        {
            items = new List<string>();
        }

        public ICollection<string> Items => items;
    }
}
