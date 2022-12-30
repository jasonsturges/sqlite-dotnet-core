using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqliteConsole.Infrastructure.Data;
using SqliteConsole.Infrastructure.Services;

// Configuration
var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Build();

// Database
var optionsBuilder = new DbContextOptionsBuilder<SqliteConsoleContext>()
    .UseSqlite(configuration.GetConnectionString("DefaultConnection"));
var context = new SqliteConsoleContext(optionsBuilder.Options);
context.Database.EnsureCreated();

// Services
var services = new ServiceCollection()
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder.AddConsole();
    })
    .AddSingleton(configuration)
    .AddSingleton(optionsBuilder.Options)
    .AddSingleton<IExampleService, ExampleService>()
    .AddDbContextPool<SqliteConsoleContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")))
    .BuildServiceProvider();

var logger = (services.GetService<ILoggerFactory>() ?? throw new InvalidOperationException())
    .CreateLogger<Program>();

logger.LogInformation($"Starting application at: {DateTime.Now}");

// Example Service
var service = services.GetService<IExampleService>();
service?.AddExample("Test A");
service?.AddExample("Test B");
service?.AddExample("Test C");
service?.GetExamples();