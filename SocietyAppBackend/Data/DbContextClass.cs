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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments {  get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow>Follows { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasOne(p => p.User).WithMany(u => u.Posts).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(i => i.PostId);
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Like>().HasOne(i => i.Post).WithMany(p => p.Likes).HasForeignKey(l => l.PostId);
            modelBuilder.Entity<Like>().HasOne(i => i.User).WithMany(i =>i.Likes).HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Follow>().HasOne(i => i.Follower).WithMany(i => i.Followers).HasForeignKey(i => i.FollowerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Follow>().HasOne(i => i.Following).WithMany(i => i.Followings).HasForeignKey(i => i.FollowingId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
