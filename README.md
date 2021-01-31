# SQLite .NET Core 3.1 Console App
.NET Core 3.1 Console Application using SQLite with Entity Framework and Dependency Injection

This example shows how to incorporate ASP.NET concepts such as dependency injection within a console application using [VS Code](https://code.visualstudio.com/) on Mac OS X / macOS or linux targets.

![vscode](https://labs.jasonsturges.com/coreclr/sqlite-dotnet-core.png)

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

    public class ExampleService : IExampleService
    {
        private readonly SqliteConsoleContext context;

        public ExampleService(SqliteConsoleContext sqliteConsoleContext)
        {
            context = sqliteConsoleContext;
        }

This way, the context may be used as follows:

        public void GetExamples()
        {
            var examples = context.Examples
                .OrderBy(e => e.Name)
                .ToList();

Otherwise, there's a factory method to instantiate new contexts:

        using (var context = SqliteConsoleContextFactory.Create(config.GetConnectionString("DefaultConnection")))
        {
            var examples = context.Examples
                .OrderBy(e => e.Name)
                .ToList();
        }
        
### Dependency Injection

Service classes are added to the main console application's Program.cs:

    // Services
    var services = new ServiceCollection()
        .AddSingleton<IExampleService, ExampleService>()
        .BuildServiceProvider();

Then, obtain the instance of the service as:

    var service = services.GetService<IExampleService>();

