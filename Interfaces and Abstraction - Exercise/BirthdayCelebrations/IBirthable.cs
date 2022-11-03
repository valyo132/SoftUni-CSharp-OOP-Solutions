namespace BirthdayCelebrations
{
    public interface IBirthable
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }

        public void CheckBirthYear(string year);
    }
}
