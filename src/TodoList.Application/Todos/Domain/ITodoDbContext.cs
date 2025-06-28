using Microsoft.EntityFrameworkCore;
using TodoList.Core.Context;
using ToDoList.Application.Todos.Domain;

namespace TodoList.Application.Todos.Domain;

public interface ITodoDbContext : IDbContext
{
    DbSet<Todo> Todos { get; }
}
