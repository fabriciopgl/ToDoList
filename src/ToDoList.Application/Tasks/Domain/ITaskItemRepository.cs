using CSharpFunctionalExtensions;
using ToDoList.Application.Tasks.Models;

namespace ToDoList.Application.Tasks.Domain;

public interface ITaskItemRepository
{
    Task<TaskItem?> GetTaskByIdAsync(int id, CancellationToken cancellationToken);
    Task<TaskItem> AddTask(TaskItem toAdd, CancellationToken cancellationToken);
    Task<Result<TaskItem>> UpdateTask(int taskId, string title, string description, ETaskStatus status, CancellationToken cancellationToken);
    Task<Result> DeleteTask(int taskId, CancellationToken cancellationToken);
}
