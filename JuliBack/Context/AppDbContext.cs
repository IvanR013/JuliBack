using JuliBack.Models;
using Microsoft.EntityFrameworkCore;
namespace JuliBack.Contexto
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
            Users = Set<Users>();
            Images = Set<Images>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var databaseVersion = configuration["DatabaseVersion"];

                if (string.IsNullOrEmpty(databaseVersion))
                {
                    throw new InvalidOperationException("DatabaseVersion is not configured.");
                }
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var serverVersion = new MySqlServerVersion(new Version(databaseVersion));
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Images> Images { get; set; }
    }
}
