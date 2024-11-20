using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>(); 
    public DbSet<User> Users => Set<User>(); 
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<SubForum> SubForums => Set<SubForum>();
    public DbSet<Moderator> Moderators => Set<Moderator>();
    public DbSet<Reaction> Reactions => Set<Reaction>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = forum.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Moderator>().HasKey(m => new { m.UserId, m.SubForumId });
        
        modelBuilder.Entity<Reaction>()
            .Property(r => r.isPost)
            .HasConversion<bool>();  // in database it will be stored as 0 or 1

        modelBuilder.Entity<Reaction>()
            .Property(r => r.IsLike)
            .HasConversion<bool>(); // in database it will be stored as 0 or 1
        
        
        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Post)
            .WithMany(p => p.Reactions)
            .HasForeignKey(r => r.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Comment)
            .WithMany(c => c.Reactions)
            .HasForeignKey(r => r.CommentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //TODO We need to handle every unique constraint in the business logic
    }
}