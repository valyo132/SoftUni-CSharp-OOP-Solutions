namespace Shapes
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            int raduis = int.Parse(Console.ReadLine());
            int width = int.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());

            IDrawable circle = new Circle(raduis);
            IDrawable rectangle = new Rectangle(width, height);

            circle.Draw();
            rectangle.Draw();
        }
    }
}
