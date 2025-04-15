using System.Text;

namespace Builder;

public class HtmlBuilderDemo
{
    public HtmlBuilderDemo()
    {
        Console.WriteLine("I am the HtmlBuilderDemo.");

        // Without builder would look something like this:
        /*
        var ul = new HtmlElement("ul", "");
        ul.Elements.Add(new HtmlElement("li", "Hello"));
        ul.Elements.Add(new HtmlElement("li", "World"));
        Console.WriteLine(ul.ToString());
        */

        // With builder
        var builder = new HtmlBuilder("html");

        var headBuilder = new HtmlBuilder("head");
        headBuilder.AddChild("title", "Hello World");
        builder.AddElement(headBuilder.Root);

        var bodyBuilder = new HtmlBuilder("body");
        var ulBuilder = new HtmlBuilder("ul");
        ulBuilder.AddChild("li", "Hello");
        ulBuilder.AddChild("li", "World");
        bodyBuilder.AddElement(ulBuilder.Root);
        builder.AddElement(bodyBuilder.Root);

        Console.WriteLine(builder.ToString());
    }
}

public class HtmlBuilder
{
    public HtmlElement Root = new();
    private readonly string? _rootName;

    public HtmlBuilder(string? rootName)
    {
        _rootName = rootName;
        Root.Name = rootName;
    }

    public void AddElement(HtmlElement element)
    {
        Root.Elements.Add(element);
    }

    public void AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        Root.Elements.Add(e);
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

public class HtmlElement
{
    public string? Name;
    public string? Text;
    public readonly List<HtmlElement> Elements = [];
    private const int IndentSize = 2;


    public HtmlElement()
    {
    }

    public HtmlElement(string? name, string? text)
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