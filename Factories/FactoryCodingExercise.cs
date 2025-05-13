namespace Factories;

public class FactoryCodingExercise
{
    public FactoryCodingExercise()
    {
        Console.WriteLine("I am the factoryCodingExercise");
        var pf = new PersonFactory();
        var person = pf.CreatePerson("Sindre");
        Console.WriteLine(person);
        var p2 = pf.CreatePerson("Knut");
        Console.WriteLine(p2);
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Person(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"{Id}: {Name}";
    }
}

public class PersonFactory
{
    private int _currentId;

    public Person CreatePerson(string name)
    {
        return new Person(_currentId++, name);
    }
}