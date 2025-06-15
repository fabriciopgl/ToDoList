using Microsoft.EntityFrameworkCore;
using TodoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Domain;

namespace TodoList.Infraestructure.Context;

public class TodoDbContext(DbContextOptions options) : DbContext(options), ITodoDbContext
{
    public DbSet<Todo> Todos{ get; set; }
}
