namespace BorderControl
{
    public class Robot : BaseHuman
    {
        public Robot(string name, string id) : base (name, id)
        { }

        public override void CheckId(string substringToCheck)
        {
            if (this.Id.EndsWith(substringToCheck))
                System.Console.WriteLine(Id);
        }
    }
}
