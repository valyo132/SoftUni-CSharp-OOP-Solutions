using System;

namespace Shapes
{
    public class Circle : Shape
    {
        public Circle(double radius)
        {
            this.Radius = radius;
        }

        public double Radius { get; private set; }

        public override double CalculateArea()
            => Math.PI * Math.Pow(this.Radius, 2);

        public override double CalculatePerimeter()
            => 2 * Math.PI * this.Radius;

        public override string Draw()
            => base.Draw() + this.GetType().Name;
    }
}
