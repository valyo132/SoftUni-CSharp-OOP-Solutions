namespace Cars
{
    public class Seat : ICar
    {
        public Seat(string model, string color)
        {
            this.Model = model;
            this.Color = color;
        }

        public string Model { get; set; }
        public string Color { get; set; }

        public string Start()
        {
            throw new System.NotImplementedException();
        }

        public string Stop()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
            => $"{Color} Seat {Model}";
    }
}
