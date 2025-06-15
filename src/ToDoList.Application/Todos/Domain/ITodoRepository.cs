using CSharpFunctionalExtensions;

namespace ToDoList.Application.Todos.Domain;

public interface ITodoRepository
{
    Task<Todo?> GetTaskByIdAsync(int id, CancellationToken cancellationToken);
    Task<Todo> Add(Todo toAdd, CancellationToken cancellationToken);
    Task Update(CancellationToken cancellationToken);
    Task<Result> Delete(int taskId, CancellationToken cancellationToken);
}
