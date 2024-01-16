using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infraestructure.Context;

public class TaskItemDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }
}
