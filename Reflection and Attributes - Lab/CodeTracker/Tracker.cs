namespace AuthorProblem
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type type = typeof(StartUp);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                if (method.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorAttribute)))
                {
                    var attribbutes = method.GetCustomAttributes(false);
                    foreach (AuthorAttribute att in attribbutes)
                    {
                        Console.WriteLine($"{method.Name} is written by {att.Name}");
                    }
                }
            }
        }
    }
}
