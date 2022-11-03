using BirthdayCelebrations;

namespace BirthdayCelebrations
{
    public class Citizent : IBirthable, IIdentifiable
    {
        public Citizent(string name, string id, int age, string birthDate)
        {
            this.Name = name;
            this.Id = id;
            this.Age = age;
            this.BirthDate = birthDate;
        }

        public string Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }

        public void CheckBirthYear(string year)
        {
            if (this.BirthDate.EndsWith(year))
                System.Console.WriteLine(BirthDate);
        }
    }
}
