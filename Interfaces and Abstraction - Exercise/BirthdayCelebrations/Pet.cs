namespace BirthdayCelebrations
{
    public class Pet : IBirthable
    {
        public Pet(string name, string birthDate)
        {
            this.Name = name;
            this.BirthDate = birthDate;
        }

        public string Name { get; set; }
        public string BirthDate { get; set; }

        public void CheckBirthYear(string year)
        {
            if (this.BirthDate.EndsWith(year))
                System.Console.WriteLine(BirthDate);
        }
    }
}
