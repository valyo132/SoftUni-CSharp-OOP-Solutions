namespace CollectionHierarchy
{
    using CollectionHierarchy.Interfaces;
    using System;
    using System.Collections.Generic;

    public class AddCollection : IAdd
    {
        public AddCollection()
        {
            this.Collection = new List<string>(100);
        }

        private List<string> Collection { get; set; }

        public int Add(string item)
        {
            this.Collection.Add(item);
            return this.Collection.Count - 1;
        }
    }
}
