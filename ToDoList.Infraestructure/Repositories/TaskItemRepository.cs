using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Task.Abstraction;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Models;
using ToDoList.Infraestructure.Context;

namespace ToDoList.Infraestructure.Repositories;

public class TaskItemRepository(TaskItemDbContext dbContext) : ITaskItemRepository
{
    public async Task<TaskItem?> GetTaskByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext
        .TaskItems
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<TaskItem> AddTask(TaskItem toAdd, CancellationToken cancellationToken)
    {
        dbContext.TaskItems.Add(toAdd);
        await dbContext.SaveChangesAsync(cancellationToken);
        return toAdd;
    }

    public async Task<Result<TaskItem>> UpdateTask(int taskId, string title, string description, ETaskStatus status, CancellationToken cancellationToken)
    {
        var taskItem = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
        if (taskItem == null)
            return Result.Failure<TaskItem>("Fail to find Task to update");

        taskItem.Update(title, description, status);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(taskItem);
    }

    public async Task<Result> DeleteTask(int taskId, CancellationToken cancellationToken)
    {
        var task = await dbContext.TaskItems.FindAsync([taskId, cancellationToken], cancellationToken: cancellationToken);
        if (task is null) 
            return Result.Failure("Cannot find Task to delete");

        dbContext.TaskItems.Remove(task);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
