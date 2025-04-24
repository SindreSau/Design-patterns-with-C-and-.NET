using System.Text;

namespace Builder;

public class BuilderExercise
{
    public BuilderExercise()
    {
        var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
        Console.WriteLine(cb);
    }
}

public class CodeBuilder
{
    private readonly string ClassName;
    private readonly List<Field> _fields = [];

    public CodeBuilder(string className)
    {
        ClassName = className;
    }

    private class Field(string type, string name)
    {
        public override string ToString()
        {
            return $"{type} {name}";
        }
    }


    public CodeBuilder AddField(string @string, string type)
    {
        _fields.Add(new Field(type, @string));

        return this;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder
            .AppendLine($"public class {ClassName}")
            .AppendLine("{");
        foreach (var field in _fields)
            stringBuilder
                .AppendLine($"  public {field.ToString()}");
        stringBuilder
            .AppendLine("}");


        return stringBuilder.ToString();
    }
}