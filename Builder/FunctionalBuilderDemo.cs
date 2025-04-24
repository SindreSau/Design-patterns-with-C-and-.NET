namespace Builder;

public class FunctionalBuilderDemo
{
    public FunctionalBuilderDemo()
    {
        Console.WriteLine("I am the Function Builder program");

        var person = new PersonBuilder().Called("Sindre")
            .WorksAs("Developer")
            .Build();
        Console.WriteLine(person);
    }
}

internal class Person
{
    public string Name, Position;

    public override string ToString()
    {
        return $"Name: {Name}, Position: {Position}";
    }
}

internal static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
    {
        return builder.Do(p => p.Position = position);
    }
}

// internal sealed class PersonBuilder
// {
//     private readonly List<Func<Person, Person>> _actions = [];
//
//     public PersonBuilder Called(string name)
//     {
//         return Do(p => p.Name = name);
//     }
//
//     public PersonBuilder Do(Action<Person> action)
//     {
//         return AddAction(action);
//     }
//
//     public Person Build()
//     {
//         // Aggregate applies each function in the list to the Person object
//         return _actions.Aggregate(new Person(), (p, f) => f(p));
//     }
//
//     private PersonBuilder AddAction(Action<Person> action)
//     {
//         _actions.Add(p =>
//         {
//             action(p);
//             return p;
//         });
//         return this;
//     }
// }

internal sealed class PersonBuilder
    : FunctionBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name)
    {
        return Do(p => p.Name = name);
    }
}

public abstract class FunctionBuilder<TSubject, TSelf>
    where TSelf : FunctionBuilder<TSubject, TSelf> where TSubject : new()
{
    private readonly List<Func<TSubject, TSubject>> _actions = [];

    public TSelf Do(Action<TSubject> action)
    {
        return AddAction(action);
    }

    public TSubject Build()
    {
        // Aggregate applies each function in the list to the TSubject object
        return _actions.Aggregate(new TSubject(), (p, f) => f(p));
    }

    private TSelf AddAction(Action<TSubject> action)
    {
        _actions.Add(p =>
        {
            action(p);
            return p;
        });
        return (TSelf)this;
    }
}