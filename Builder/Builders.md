# What are Builders and why do we need them?

Some objects are simple and can easily be creating with a simple constructor. However, some objects are more complex,
making it easier in the long run to use a builder pattern to implement the object step by step.

# Example -> ASP.NET Program.cs file

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Create a builder for the web application
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

var app = builder.Build();

```
