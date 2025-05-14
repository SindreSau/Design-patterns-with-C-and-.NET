using System.Text.Json;
using System.Xml.Serialization;

namespace Prototype;

// Move Person class to top level for serialization
public class Person
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }

    public Person() // Xml Serializer needs a default constructor
    {
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}

public class SerializedCopyDemo
{
    public SerializedCopyDemo()
    {
        Console.WriteLine("Serialized Copy Demo");
        var person = new Person("John Doe", 30);

        // Create copies using both methods
        var person2 = person.DeepCopyXml();
        person2!.Name = "XML Copy";

        var person3 = person.DeepCopy();
        person3!.Name = "JSON Copy";

        // Demonstrate the copies work
        Console.WriteLine($"Original: {person}");
        Console.WriteLine($"XML Copy: {person2}");
        Console.WriteLine($"JSON Copy: {person3}");

        // Demonstrate that they're independent copies
        person.Name = "Changed Original";
        Console.WriteLine($"\nAfter changing original:");
        Console.WriteLine($"Original: {person}");
        Console.WriteLine($"XML Copy: {person2}");
        Console.WriteLine($"JSON Copy: {person3}");
    }
}

public static class ExtensionMethods
{
    // JSON-based deep copy (replacement for BinaryFormatter)
    public static T? DeepCopy<T>(this T self)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(self, options);
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    // XML-based deep copy
    public static T? DeepCopyXml<T>(this T self)
    {
        using var ms = new MemoryStream();
        var s = new XmlSerializer(typeof(T));
        s.Serialize(ms, self);
        ms.Position = 0;
        return (T)s.Deserialize(ms)!;
    }
}