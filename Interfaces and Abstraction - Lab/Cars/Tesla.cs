namespace Cars
{
    public class Tesla : ICar, IElectricCar
    {
        public Tesla(string model, string color, int battery)
        {
            this.Model = model;
            this.Color = color;
            this.Battery = battery;
        }

        public string Model { get; set; }
        public string Color { get; set; }
        public int Battery { get; set; }

        public string Start()
        {
            throw new System.NotImplementedException();
        }

        public string Stop()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
            => $"{Color} Tesla {Model} with {Battery} Batteries";
    }
}
