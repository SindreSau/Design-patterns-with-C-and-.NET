using MoreLinq;
using NUnit.Framework;

namespace Singleton;

public class SingletonDatabaseDemo
{
    public SingletonDatabaseDemo()
    {
        var db = SingletonDatabase.Instance;
        Console.WriteLine($"Tokyo has {db.GetPopulation("Tokyo"):N0} people");
        Console.WriteLine($"Seoul has {db.GetPopulation("Seoul"):N0} people");
        Console.WriteLine($"New York has {db.GetPopulation("New York"):N0} people");
    }
}

internal interface IDatabase
{
    int GetPopulation(string name);
}

internal class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> capitals;
    private static SingletonDatabase instance = new();

    private SingletonDatabase()
    {
        Console.WriteLine("Intializing database");

        // Simulate database - including capital and population amount
        capitals = File.ReadAllLines("capitals.txt")
            .Batch(2)
            .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1))
            );
    }

    public int GetPopulation(string name)
    {
        return capitals[name];
    }

    public static SingletonDatabase Instance => instance;
}

// Test
[TestFixture]
internal class SingletonTests
{
    [Test]
    public void IsSingletonTest()
    {
        var db = SingletonDatabase.Instance;
        var db2 = SingletonDatabase.Instance;
        Assert.That(db, Is.SameAs(db2));
    }
}