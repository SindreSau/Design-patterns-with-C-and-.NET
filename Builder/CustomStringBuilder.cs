namespace Builder;

public class CustomStringBuilder
{
    private readonly List<char> _string = [];

    public CustomStringBuilder()
    {
    }

    public CustomStringBuilder(string? s)
    {
        _string = s?.ToCharArray().ToList() ?? [];
    }

    public CustomStringBuilder Append(string? s)
    {
        _string.AddRange(s?.ToCharArray() ?? []);
        return this;
    }

    public CustomStringBuilder AppendLine(string? s)
    {
        _string.AddRange(s?.ToCharArray() ?? []);
        return this;
    }

    public override string ToString()
    {
        return new string(_string.ToArray());
    }
}

/// <summary>
/// Optimized version of the CustomStringBuilder.
///
/// This version uses a List of chars to avoid the overhead of creating a new string for each append operation.
/// </summary>
public class CustomStringBuilderOptimized
{
    private readonly List<char> _string = [];

    public CustomStringBuilderOptimized()
    {
    }

    public CustomStringBuilderOptimized(string? s)
    {
        _string = s?.ToCharArray().ToList() ?? [];
    }

    public CustomStringBuilderOptimized Append(string? s)
    {
        _string.AddRange(s?.ToCharArray() ?? []);
        return this;
    }

    public CustomStringBuilderOptimized AppendLine(string? s)
    {
        _string.AddRange(s?.ToCharArray() ?? []);
        return this;
    }

    public override string ToString()
    {
        return new string(_string.ToArray());
    }
}