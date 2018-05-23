using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqliteConsole.Infrastructure.Data;
using SqliteConsole.Infrastructure.Services;

namespace SqliteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
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
                .AddLogging()
                .AddSingleton(configuration)
                .AddSingleton(optionsBuilder.Options)
                .AddSingleton<IExampleService, ExampleService>()
                .AddDbContextPool<SqliteConsoleContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")))
                .BuildServiceProvider();

            // Logging
            services
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Trace);

            var logger = services.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogInformation($"Starting application at: {DateTime.Now}");

            // Services
            var service = services.GetService<IExampleService>();
            service.AddExample("Test A");
            service.AddExample("Test B");
            service.AddExample("Test C");
            service.GetExamples();
        }
    }
}
