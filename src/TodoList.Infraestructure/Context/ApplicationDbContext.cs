using Microsoft.EntityFrameworkCore;
using TodoList.Application.Todos.Domain;
using TodoList.Application.Users.Domain;
using ToDoList.Application.Todos.Domain;

namespace TodoList.Infraestructure.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options), ITodoDbContext, IUserDbContext
{
    public DbSet<Todo> Todos{ get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.DueDate).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.UserId).IsRequired();

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired(false);

            // Unique constraint on email
            entity.HasIndex(e => e.Email).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
