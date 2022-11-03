namespace CollectionHierarchy
{
    using CollectionHierarchy.Interfaces;
    using System.Collections.Generic;

    public class AddRemoveCollection : IRemove
    {
        public AddRemoveCollection()
        {
            this.Collection = new List<string>(100);
        }

        private List<string> Collection { get; set; }

        public int Add(string item)
        {
            this.Collection.Insert(0, item);
            return 0;
        }

        public string RemoveItem()
        {
            string item = this.Collection[this.Collection.Count - 1]; 
            this.Collection.RemoveAt(this.Collection.Count - 1);

            return item;
        }
    }
}
