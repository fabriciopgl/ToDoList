using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Models;
using ToDoList.Infraestructure.Context;

namespace ToDoList.Infraestructure.Repositories;

public class TodoRepository(TodoDbContext dbContext) : ITodoRepository
{
    public async Task<Todo?> GetTaskByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext
            .TaskItems
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Todo> Add(Todo toAdd, CancellationToken cancellationToken)
    {
        dbContext.TaskItems.Add(toAdd);
        await dbContext.SaveChangesAsync(cancellationToken);
        return toAdd;
    }

    public async Task Update(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result> Delete(int taskId, CancellationToken cancellationToken)
    {
        var task = await dbContext.TaskItems.FindAsync([taskId, cancellationToken], cancellationToken: cancellationToken);
        if (task is null) 
            return Result.Failure("Cannot find Task to delete");

        dbContext.TaskItems.Remove(task);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
