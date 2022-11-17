namespace ValidationAttributes.Models
{
    using Attributes;

    public class Person
    {
        public Person(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
        }

        [MyRequired]
        public string FullName { get; }

        [MyRange(0, 20)]
        public int Age { get; }
    }
}
