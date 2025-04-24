using System.Diagnostics;
using Builder;

Console.WriteLine("I am the builder program.");

// new HtmlBuilderDemo();
// new HtmlBuilderDemoFluent();
// new StepwiseBuilderDemo();
// new FunctionalBuilderDemo();
// new FacetedBuilderDemo();
// new BuilderExercise();

const int iterations = 10000;
const string longString = "This is a moderately long string to append. ";
const string lineString = "This is a line to append with a newline.";

var stopwatch = new Stopwatch();

stopwatch.Start();
var sb = new CustomStringBuilder();
for (var i = 0; i < iterations; i++)
{
    sb.Append(longString);
    sb.Append($"Iteration {i}. ");
    if (i % 10 == 0) sb.AppendLine(lineString);
}

var result1 = sb.ToString(); // Store result to avoid optimizing away the work
stopwatch.Stop();
Console.WriteLine($"CustomStringBuilder completed in: {stopwatch.ElapsedMilliseconds} ms. Length: {result1.Length}");


stopwatch.Restart();
var sbo = new CustomStringBuilderOptimized();
for (var i = 0; i < iterations; i++)
{
    sbo.Append(longString);
    sbo.Append($"Iteration {i}. ");
    if (i % 10 == 0) sbo.AppendLine(lineString);
}

var result2 = sbo.ToString(); // Store result
stopwatch.Stop();
Console.WriteLine(
    $"CustomStringBuilderOptimized completed in: {stopwatch.ElapsedMilliseconds} ms. Length: {result2.Length}");