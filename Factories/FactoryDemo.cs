namespace Factories;

public class FactoryDemo
{
    public FactoryDemo()
    {
        Console.WriteLine("I am the factory demo.");

        var rect = Quadriliteral.Factory.NewRectangle(5, 10);
        var square = Quadriliteral.Factory.NewSquare(5);

        rect.PrintArea();
        square.PrintArea();
    }
}

public class Quadriliteral(double height, double width)
{
    private double _width = width, _height = height;

    public void PrintArea()
    {
        var isSquare = _width == _height;
        Console.WriteLine($"The area of the {(isSquare ? "square" : "rectangle")} is: {_width * _height}");
    }

    public abstract class Factory
    {
        public static Quadriliteral NewRectangle(double width, double height)
        {
            return new Quadriliteral(width, height);
        }

        public static Quadriliteral NewSquare(double side)
        {
            return new Quadriliteral(side, side);
        }
    }
}