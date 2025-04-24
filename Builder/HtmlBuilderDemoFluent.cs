using System.Text;

namespace Builder;

/*
Fluent interfaces are a way to make APIs easier to use by being able to chain methods together.

In this case, returning a HtmlBuilderFluent object from the AddChild method allows us to chain calls together. This
was not possible in the HtmlBuilder class, where we had to call AddChild separately.
*/

public class HtmlBuilderDemoFluent
{
    public HtmlBuilderDemoFluent()
    {
        Console.WriteLine("I am the HtmlBuilderFluentDemo.");

        // Without builder would look something like this:
        /*
        var ul = new HtmlElement("ul", "");
        ul.Elements.Add(new HtmlElement("li", "Hello"));
        ul.Elements.Add(new HtmlElement("li", "World"));
        Console.WriteLine(ul.ToString());
        */

        // With builder
        var builder = new HtmlBuilderFluent("html");

        var headBuilder = new HtmlBuilderFluent("head");
        headBuilder.AddChild("title", "Hello World");
        builder.AddElement(headBuilder.Root);

        var bodyBuilder = new HtmlBuilderFluent("body");
        var ulBuilder = new HtmlBuilderFluent("ul");
        ulBuilder
            .AddChild("li", "Hello")
            .AddChild("li", "World");
        bodyBuilder.AddElement(ulBuilder.Root);
        builder.AddElement(bodyBuilder.Root);

        Console.WriteLine(builder.ToString());
    }
}

internal class HtmlBuilderFluent
{
    public HtmlElement Root = new();
    private readonly string? _rootName;

    public HtmlBuilderFluent(string? rootName)
    {
        _rootName = rootName;
        Root.Name = rootName;
    }

    public void AddElement(HtmlElement element)
    {
        Root.Elements.Add(element);
    }

    public HtmlBuilderFluent AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        Root.Elements.Add(e);
        return this;
    }

    public override string ToString()
    {
        return Root.ToString();
    }

    public void Clear()
    {
        Root = new HtmlElement
        {
            Name = _rootName
        };
    }
}

public class HtmlElementFluent
{
    public string? Name;
    public string? Text;
    public readonly List<HtmlElement> Elements = [];
    private const int IndentSize = 2;


    public HtmlElementFluent()
    {
    }

    public HtmlElementFluent(string? name, string? text)
    {
        Name = name;
        Text = text;
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', IndentSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append(new string(' ', IndentSize * (indent + 1)));
            sb.AppendLine(Text);
        }

        foreach (var e in Elements) sb.Append(e.ToStringImpl(indent + 1));

        sb.AppendLine($"{i}</{Name}>");

        return sb.ToString();
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }
}