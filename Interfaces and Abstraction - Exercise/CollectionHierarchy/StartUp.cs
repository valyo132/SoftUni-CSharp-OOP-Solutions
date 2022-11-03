namespace CollectionHierarchy
{
    using CollectionHierarchy.Interfaces;
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IAdd addCollection = new AddCollection();
            IRemove addRemoveCollection = new AddRemoveCollection();
            IMyList myListCollection = new MyList();

            string[] input = Console.ReadLine().Split();

            GetAddResults(addCollection, addRemoveCollection, myListCollection, input);

            GetRemoveResults(addRemoveCollection, myListCollection);
        }

        private static void GetRemoveResults(IRemove addRemoveCollection, IMyList myListCollection)
        {
            int countToRemove = int.Parse(Console.ReadLine());

            for (int i = 0; i < countToRemove; i++)
                Console.Write(addRemoveCollection.RemoveItem() + " ");

            Console.WriteLine();

            for (int i = 0; i < countToRemove; i++)
                Console.Write(myListCollection.RemoveItem() + " ");
        }

        private static void GetAddResults(IAdd addCollection, IRemove addRemoveCollection, IMyList myListCollection, string[] input)
        {
            foreach (var item in input)
                Console.Write(addCollection.Add(item) + " ");

            Console.WriteLine();

            foreach (var item in input)
                Console.Write(addRemoveCollection.Add(item) + " ");

            Console.WriteLine();

            foreach (var item in input)
                Console.Write(myListCollection.Add(item) + " ");

            Console.WriteLine();
        }
    }
}
