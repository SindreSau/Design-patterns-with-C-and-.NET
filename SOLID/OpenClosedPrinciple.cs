namespace SOLID;

/*
Open-Closed Principle:
- Software entities (classes, modules, functions, etc.) should be open for extension but closed for modification.
- This means that the behavior of a module can be extended without modifying its source code.
- In practice, this often means using interfaces or abstract classes to allow for new implementations to be added without changing existing code.
*/

public class OpenClosedPrinciple
{
    public OpenClosedPrinciple()
    {
        Console.WriteLine("Open Closed Principle");

        var apple = new Product("Apple", Color.Red, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var car = new Product("Car", Color.Blue, Size.Large);
        var house = new Product("House", Color.Blue, Size.Yuge);

        var products = new List<Product> { apple, tree, car, house };

        Console.WriteLine("Products of size Large:");
        var largeProducts = ProductFilterOld.FilterBySize(products, Size.Large);
        foreach (var p in largeProducts) Console.WriteLine($"- {p.Name} is {p.Color} and {p.Size}");

        // New method
        Console.WriteLine("Better way, products of color Blue: ");
        var bf = new BetterFilter();
        foreach (var p in bf.Filter(products, new ColorSpecification(Color.Blue)))
            Console.WriteLine($" - {p.Name} is {p.Color}");

        // And specification
        Console.WriteLine("AndSpecification - blue and yuge");
        foreach (var p in bf.Filter(products,
                     new AndSpecification<Product>(
                         new ColorSpecification(Color.Blue),
                         new SizeSpecification(Size.Yuge)
                     )))
            Console.WriteLine($" - {p.Name} is both {p.Color} and {p.Size}");
    }
}

public enum Color
{
    Red,
    Green,
    Blue
}

public enum Size
{
    Small,
    Medium,
    Large,
    Yuge // Regards, Donald Trump
}

public class Product
{
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        Name = name;
        Color = color;
        Size = size;
    }
}

/// <summary>
/// Provides filtering methods for products. This implementation violates the Open/Closed Principle
/// because adding new filtering criteria requires modifying this class.
/// </summary>
internal abstract class ProductFilterOld
{
    public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (var p in products)
            if (p.Size == size)
                yield return p;
    }

    public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (var p in products)
            if (p.Color == color)
                yield return p;
    }
}

public interface ISpecification<T>
{
    bool IsSatisfied(T t);
}

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

public class ColorSpecification(Color color) : ISpecification<Product>
{
    public bool IsSatisfied(Product t)
    {
        return t.Color == color;
    }
}

public class SizeSpecification(Size size) : ISpecification<Product>
{
    public bool IsSatisfied(Product t)
    {
        return t.Size == size;
    }
}

public class AndSpecification<T>(ISpecification<T> first, ISpecification<T> second) : ISpecification<T>
{
    private readonly ISpecification<T> _first = first ?? throw new ArgumentNullException(nameof(first));
    private readonly ISpecification<T> _second = second ?? throw new ArgumentNullException(nameof(second));

    public bool IsSatisfied(T t)
    {
        return _first.IsSatisfied(t) && _second.IsSatisfied(t);
    }
}

/// <summary>
/// Implements the <see cref="IFilter{T}"/> interface to provide filtering functionality
/// that adheres to the Open/Closed Principle.
/// </summary>
/// <remarks>
/// This class is open for extension because new filtering criteria can be added
/// by creating new implementations of the <see cref="ISpecification{T}"/> interface,
/// without modifying the existing code in this class.
/// </remarks>
public class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
        foreach (var i in items)
            if (spec.IsSatisfied(i))
                yield return i;
    }
}