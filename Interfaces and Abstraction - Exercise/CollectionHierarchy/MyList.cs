namespace CollectionHierarchy
{
    using CollectionHierarchy.Interfaces;
    using System.Collections.Generic;

    public class MyList : IMyList
    {
        public MyList()
        {
            this.Collection = new List<string>(100);
        }

        public List<string> Collection { get; set; }
        public int Used { get { return this.Collection.Count; } }

        public int Add(string item)
        {
            this.Collection.Insert(0, item);
            return 0;
        }

        public string RemoveItem()
        {
            string item = this.Collection[0];
            this.Collection.RemoveAt(0);

            return item;
        }
    }
}
