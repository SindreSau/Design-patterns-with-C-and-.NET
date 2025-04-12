using System.Diagnostics;

namespace SOLID;

public class SingleResponsibilityPrinciple
{
    public SingleResponsibilityPrinciple()
    {
        Console.WriteLine("Single Responsibility Principle");

        Journal j = new();
        j.AddEntry("This is my first day using the journal.");
        j.AddEntry("Today i learned about the Single Responsibility Principle");

        var filename = "journal.txt";
        Persistence.SaveToFile(j,filename);
        Persistence.ShowFile(filename);
    }
}

internal class Journal
{
    private readonly List<string> _entries = new();

    public void AddEntry(string text)
    {
        _entries.Add(text);
    }

    public override string ToString()
    {
        return "My Journal: \n" + string.Join(Environment.NewLine, _entries);
    }
}

internal class Persistence
{
    public static void SaveToFile(Journal journal, string filename)
    {
        File.WriteAllText(filename, journal.ToString());
        Console.WriteLine($"Journal saved to {Path.GetFullPath(filename)}");
    }

    public static void ShowFile(string filename)
    {
        // For macOS
        var startInfo = new ProcessStartInfo
        {
            FileName = "open",
            Arguments = filename,
            UseShellExecute = false
        };

        Process.Start(startInfo);
    }
}