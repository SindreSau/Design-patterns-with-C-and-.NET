namespace Factories;

/*
*Factory Method*
Factory methods are great for creating abstract classes with concrete implementations.
You can for example create various types of "rectangles" from the same "rectangle" class.
var rectangle = Rectangle.NewRectangle(5, 10);
var square = Rectangle.NewSquare(5);
*/

public class FactoryMethodDemo
{
    public FactoryMethodDemo()
    {
        Console.WriteLine("I am the factory method program.");

        var cartesianPoint = Point.NewCartesianPoint(3, 4);
        var polarPoint = Point.NewPolarPoint(5, Math.PI / 10);
        Console.WriteLine(cartesianPoint);
        Console.WriteLine(polarPoint);

        var rectangle = Rectangle.NewRectangle(5, 10);
        var square = Rectangle.NewSquare(5);

        Console.WriteLine($"Area of rectangle: {Rectangle.Area(rectangle)}");
        Console.WriteLine($"Area of square: {Rectangle.Area(square)}");
    }

    public async Task RunAsync()
    {
        await AsyncFactoryMethod.CreateAsync();
        Console.WriteLine("Async factory method created");
    }

    private class Point
    {
        private readonly double _x, _y;

        private Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        // Factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        // Factory method
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public override string ToString()
        {
            return $"Point({_x:G4}, {_y:G4})";
        }
    }

    private class Rectangle
    {
        private readonly double _width, _height;

        private Rectangle(double width, double height)
        {
            _width = width;
            _height = height;
        }

        // Factory method
        public static Rectangle NewRectangle(double width, double height)
        {
            return new Rectangle(width, height);
        }

        // Factory method
        public static Rectangle NewSquare(double side)
        {
            return new Rectangle(side, side);
        }

        public static double Area(Rectangle rectangle)
        {
            return rectangle._width * rectangle._height;
        }
    }

    private class AsyncFactoryMethod
    {
        private AsyncFactoryMethod()
        {
            // Constructor can't be async
            Console.WriteLine("Initialized");
        }

        private async Task<AsyncFactoryMethod> InitAsync()
        {
            await Task.Delay(1500);
            return this;
        }

        public static Task<AsyncFactoryMethod> CreateAsync()
        {
            var result = new AsyncFactoryMethod();
            return result.InitAsync();
        }
    }
}