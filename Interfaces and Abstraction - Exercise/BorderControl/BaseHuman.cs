namespace BorderControl
{
    public abstract class BaseHuman
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public BaseHuman(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        public abstract void CheckId(string substringToCheck);
    }
}
