namespace BorderControl
{
    public class Citizent : BaseHuman
    {
        public Citizent(string name, int age, string id) : base(name, id)
        {
            this.Age = age;
        }

        public int Age { get; set; }

        public override void CheckId(string substringToCheck)
        {
            if (this.Id.EndsWith(substringToCheck))
                System.Console.WriteLine(Id);
        }
    }
}
