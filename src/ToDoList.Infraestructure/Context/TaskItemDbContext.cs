using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Tasks.Domain;

namespace ToDoList.Infraestructure.Context;

public class TaskItemDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }
}
