using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Infraestructure.Repositories;

public class TodoRepository(ITodoDbContext dbContext) : ITodoRepository
{
    public async Task<Todo?> GetTaskByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Todos
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Todo> Add(Todo toAdd, CancellationToken cancellationToken)
    {
        dbContext.Todos.Add(toAdd);
        await dbContext.SaveChangesAsync(cancellationToken);
        return toAdd;
    }

    public async Task Update(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result> Delete(Todo todo, CancellationToken cancellationToken)
    {
        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
