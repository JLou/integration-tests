using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Brenda.Models
{
    public class BrendaContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public BrendaContext(DbContextOptions<BrendaContext> options)
            : base(options)
        {

        }

        public DbSet<Forecast> Forecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forecast>().ToTable("Forecast");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLoggerFactory(MyLoggerFactory);

    }
}