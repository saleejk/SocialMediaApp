using Microsoft.EntityFrameworkCore;
using SocietyAppBackend.ModelEntity;

namespace SocietyAppBackend.Data
{
    public class DbContextClass:DbContext
    {
        public readonly IConfiguration _dbContext;
        public readonly string connectionString;
        public DbContextClass(IConfiguration dbContext)
        {
            _dbContext = dbContext;
            connectionString = _dbContext["ConnectionStrings:DefaultConnection"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<User> UserTable { get; set; }
    }
}
