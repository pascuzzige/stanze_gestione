using Microsoft.EntityFrameworkCore;
using Stanze_management.Data.Entities;
using Stanze_management.DBContexts;


namespace Stanze_management.DBContexts
{
    public class Stanze_dbContext:DbContext
    {
        public Stanze_dbContext(DbContextOptions<Stanze_dbContext> options):base(options)
        {
            
        }

        public DbSet<Uffici> Uffici { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration= new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("dbCon");
            optionsBuilder.UseSqlServer(connectionString);
            
        }

    }
}
