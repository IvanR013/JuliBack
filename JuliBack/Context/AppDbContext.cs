using JuliBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace JuliBack.Contexto
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        
        : base(options)
        {
            this.configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var serverVersion = new MySqlServerVersion(new Version(configuration["DatabaseVersion"]));
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Images> Images { get; set; }
    }
}
