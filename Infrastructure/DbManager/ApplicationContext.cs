using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbManager;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<User> Users {get;set;}
    public DbSet<UserTask> Tasks {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("users");

        modelBuilder.Entity<UserTask>()
            .ToTable("tasks").HasKey(t => t.ID);

        modelBuilder.Entity<UserTask>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}