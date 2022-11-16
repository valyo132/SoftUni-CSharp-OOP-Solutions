namespace AuthorProblem
{
    using System;

    [Author("Victor")]
    public class StartUp
    {
        static void Main(string[] args)
        {
            var tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
        }

        [Author("Valyo")]
        public void Test() { }
    }
}
