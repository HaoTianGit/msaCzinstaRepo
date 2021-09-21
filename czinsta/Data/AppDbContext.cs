
using czinsta.Models;
using Microsoft.EntityFrameworkCore;

namespace czinsta.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Rating> Ratings { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Account)
                .WithMany(s => s.Posts)
                .HasForeignKey(p => p.AccountId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Account)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Account)
                .WithMany(s => s.Ratings)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.PostId);
        }
    }
}
