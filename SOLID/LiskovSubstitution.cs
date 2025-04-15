namespace SOLID;

/*
Liskov Substitution Principle (LSP)

Defintion:
Design guideline stating that if a class S is a subtype of T, objects of type T should be replaceable with objects of
type S without altering the correctness of the program. This ensures that subclasses behave consistently with the
expectations set by their base classes, enabling more reliable and maintainable code.

Example:
A Square subclass of Rectangle must preserve its behavior. Methods expecting a Rectangle—such as area
calculation—should work with a Square instance without extra handling.
*/

public class LiskovSubstitution
{
    public static int Area(IQuadrilateral q)
    {
        return q.Width * q.Height;
    }

    public LiskovSubstitution()
    {
        Console.WriteLine("I am LiskovSubstitution:\n");

        var rc = new Rectangle(2, 3);
        Console.WriteLine($"{rc} has an area of {Area(rc)}");

        var sq = new Square(3);
        Console.WriteLine($"{sq} has an area of {Area(sq)}");
    }
}

public interface IQuadrilateral
{
    int Width { get; set; }
    int Height { get; set; }
}

public class Rectangle(int width, int height) : IQuadrilateral
{
    public virtual int Width { get; set; } = width; // This will be defaulted to 0 if not set
    public virtual int Height { get; set; } = height; // This will be defaulted to 0 if not set

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
}

public class Square(int side) : IQuadrilateral
{
    public int Width { get; set; } = side;
    public int Height { get; set; } = side;

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
}

public class SquareFromVideo(int side) : Rectangle(side, side)
{
    public override int Width
    {
        get => base.Width;
        set
        {
            base.Width = value;
            base.Height = value;
        }
    }

    public override int Height
    {
        get => base.Height;
        set
        {
            base.Width = value;
            base.Height = value;
        }
    }
}