using Microsoft.EntityFrameworkCore;
using SqliteConsole.Core.Entities;

namespace SqliteConsole.Infrastructure.Data
{

    /// <summary>
    /// Entity framework context
    /// </summary>
    public class SqliteConsoleContext : DbContext
    {
        public SqliteConsoleContext(DbContextOptions<SqliteConsoleContext> options)
            : base(options)
        { }

        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Example>()
                .Property(e => e.Name)
                .HasColumnType("varchar(512)");
        }
    }

    public static class SqliteConsoleContextFactory
    {
        public static SqliteConsoleContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteConsoleContext>();
            optionsBuilder.UseSqlite(connectionString);

            var context = new SqliteConsoleContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}