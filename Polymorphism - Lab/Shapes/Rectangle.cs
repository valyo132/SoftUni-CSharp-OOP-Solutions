namespace Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }

        public double Height { get; private set; }
        public double Width { get; private set; }

        public override double CalculateArea()
            => Height * Width;

        public override double CalculatePerimeter()
            => 2 * (Height + Width);

        public override string Draw()
            => base.Draw() + this.GetType().Name;
    }
}
