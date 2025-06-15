using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Infraestructure.Context;

public class TodoDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Todo> TaskItems { get; set; }
}
