using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqliteConsole.Infrastructure.Data;

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
                .AddDbContextPool<SqliteConsoleContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")))
                .BuildServiceProvider();

            // Logging
            services
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Trace);

            var logger = services.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogInformation($"Starting application at: {DateTime.Now}");
        }
    }
}
