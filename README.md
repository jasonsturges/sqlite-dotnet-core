# SQLite .NET 7 Console App
.NET 7.0 Console Application using SQLite with Entity Framework and Dependency Injection

This example shows how to incorporate ASP.NET concepts such as dependency injection within a console application using [VS Code](https://code.visualstudio.com/) on macOS or linux targets.

![vscode](https://user-images.githubusercontent.com/1213591/106406012-9d305c00-63fd-11eb-98e0-c2a0fca08afe.png)

## Project Structure

This solution is divided into three projects:

- **SqliteConsole**: The main console application
- **SqliteConsole.Core**: Entity models
- **SqliteConsole.Infrasture**: Entity framework database context and service classes

## Concepts

The following concepts are demonstrated within this example console application project:
- SQLite Entity Framework
- Dependency Injection

### SQLite Entity Framework

Using dependency injection, the database context can be passed to a constructor of a class:

```cs
public class ExampleService : IExampleService
{
    private readonly SqliteConsoleContext context;

    public ExampleService(SqliteConsoleContext sqliteConsoleContext)
    {
        context = sqliteConsoleContext;
    }
```

This way, the context may be used as follows:

```cs
    public void GetExamples()
    {
        var examples = context.Examples
            .OrderBy(e => e.Name)
            .ToList();
```

Otherwise, there's a factory method to instantiate new contexts:

```cs
    using (var context = SqliteConsoleContextFactory.Create(config.GetConnectionString("DefaultConnection")))
    {
        var examples = context.Examples
            .OrderBy(e => e.Name)
            .ToList();
    }
```
        
### Dependency Injection

Service classes are added to the main console application's Program.cs:

```cs
// Services
var services = new ServiceCollection()
    .AddSingleton<IExampleService, ExampleService>()
    .BuildServiceProvider();
```

Then, obtain the instance of the service as:

```cs
var service = services.GetService<IExampleService>();
```
