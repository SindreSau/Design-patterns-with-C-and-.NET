namespace SOLID;

/*
Dependency Inversion Principle (DIP)

High-level modules should not depend on low-level modules. Both should depend on abstractions.

Here it is being followed by:
1. Defining an interface (IRelationshipBrowser) that abstracts the relationship operations.
2. The high-level module (Research) depends on the abstraction (IRelationshipBrowser) instead of the concrete
implementation (Relationships).
3. The low-level module (Relationships) implements the abstraction, allowing for flexibility and easier testing.
*/

public class DependencyInversionPrinciple
{
    public DependencyInversionPrinciple()
    {
        Console.WriteLine("I am the Dependency Inversion Principle program.\n");

        // Setup relationships
        var parent = new Person("John");
        var child1 = new Person("Chris");
        var child2 = new Person("Mary");

        // Low-level module
        IRelationshipBrowser relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        // High-level module
        var research = new Research(relationships);
        // Research uses the abstraction, not the concrete Relationships class
    }
}

public enum Relationship
{
    Parent,
    Child,
    Sibling
}

public class Person(string name)
{
    public readonly string Name = name;
}

// The abstraction both modules depend on
public interface IRelationshipBrowser
{
    void AddParentAndChild(Person parent, Person child);
    IEnumerable<Person> FindAllChildrenOf(string name);
}

// Low-level module
public class Relationships : IRelationshipBrowser
{
    private readonly List<(Person, Relationship, Person)> _relations = new();

    public void AddParentAndChild(Person parent, Person child)
    {
        _relations.Add((parent, Relationship.Parent, child));
        _relations.Add((child, Relationship.Child, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        return _relations
            .Where(r => r.Item1.Name == name && r.Item2 == Relationship.Parent)
            .Select(r => r.Item3);
    }
}

// High-level module
public class Research
{
    // Constructor depends on abstraction, not concrete implementation
    public Research(IRelationshipBrowser browser)
    {
        // Find all of John's children
        var children = browser.FindAllChildrenOf("John");

        foreach (var child in children) Console.WriteLine($"John has a child called {child.Name}");
    }
}