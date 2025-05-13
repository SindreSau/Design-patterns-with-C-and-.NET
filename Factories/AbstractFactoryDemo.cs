namespace Factories;

public class AbstractFactoryDemo
{
    public AbstractFactoryDemo()
    {
        Console.WriteLine("I am the Abstract Factory Demo.");
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink();
        drink.Consume();
    }
}

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This tea is nice!");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("Mmmm, lovely coffee!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

public class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Put in tea bag, boil water, pour {amount}ml, add lemon, enjoy!");
        return new Tea();
    }
}

public class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Grind some beans, boil water, pour {amount}ml, add cream, enjoy!");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    // public enum AvailableDrink
    // {
    //     Coffee,
    //     Tea
    // }
    //
    // private Dictionary<AvailableDrink, IHotDrinkFactory> _factories = new();
    //
    // public HotDrinkMachine()
    // {
    //     foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //     {
    //         var factoryTypeName = $"Factories.{Enum.GetName(typeof(AvailableDrink), drink)}Factory";
    //         var factoryType = Type.GetType(factoryTypeName);
    //
    //         if (factoryType == null) throw new Exception($"Cannot find factory type for {factoryTypeName}");
    //
    //         var factory = (IHotDrinkFactory)Activator.CreateInstance(factoryType);
    //         _factories.Add(drink, factory);
    //     }
    // }
    //
    // public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    // {
    //     return _factories[drink].Prepare(amount);
    // }

    private readonly List<Tuple<string, IHotDrinkFactory>> _factories = [];

    public HotDrinkMachine()
    {
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                _factories.Add(Tuple.Create(
                    t.Name.Replace("Factory", string.Empty),
                    (IHotDrinkFactory)Activator.CreateInstance(t)!
                ));
    }

    public IHotDrink MakeDrink()
    {
        Console.WriteLine("Available drinks: ");
        for (var i = 0; i < _factories.Count; i++)
        {
            var factory = _factories[i];
            Console.WriteLine($"{i}: {factory.Item1}");
        }

        while (true)
        {
            string? s;
            if ((s = Console.ReadLine()) != null
                && int.TryParse(s, out var i)
                && i >= 0
                && i < _factories.Count)
            {
                Console.WriteLine("Specify maount: ");
                s = Console.ReadLine();
                if (s != null && int.TryParse(s, out var amount) && amount > 0)
                    return _factories[i].Item2.Prepare(amount);
            }

            Console.WriteLine("Incorrect input, try again.");
        }
    }
}