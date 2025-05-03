using JuliBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace JuliBack.Contexto;

public class AppDbContext : DbContext
{
    private readonly IConfiguration configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)

    : base(options) => this.configuration = configuration;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var databaseVersion = configuration["DatabaseVersion"] ?? throw new InvalidOperationException("DatabaseVersion is not configured.");
            var serverVersion = new MySqlServerVersion(new Version(databaseVersion));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Images> Images { get; set; }
}

