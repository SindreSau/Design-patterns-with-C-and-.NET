# Factories

## Why?

Constructors are not descriptive:

- They are not self-documenting.
- Can't have different constructors for different use cases.
- Can turn into "optional parameter hell" if you have many optional parameters.

Object creation (non-piecewise unlike builders) can be outsourced to:
- Separate function (factory method)
- Existing in a class (factory)

*Wholesale creation of objects!*

## Point example
Why this is bad:
- Constructor has to match the name of the class.
- The documentation would be essential to understand the constructor.

```csharp
public enum CoordinateSystem
{
    Cartesian,
    Polar
}

public class Point
{
    private double x, y;

    public Point(double x, double y,
        CoordinateSystem coordinateSystem = CoordinateSystem.Cartesian)
    {
        switch (system)
        {
            case CoordinateSystem.Cartesian:
                this.x = x;
                this.y = y;
                break;
            case CoordinateSystem.Polar:
                this.x = x * Math.Cos(y);
                this.y = x * Math.Sin(y);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(system), system, null);
        }
    }
}
```
