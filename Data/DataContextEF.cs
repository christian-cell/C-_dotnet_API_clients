using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ClientsApi.Models;
using System.Reflection.Emit;
using System.Configuration;

namespace ClientsApi.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                         optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MyFirstAPIschema");

            modelBuilder.Entity<Client>()
                .ToTable("Clients", "MyFirstAPIschema")
                .HasKey(c => c.ClientId);
        }
    }
}