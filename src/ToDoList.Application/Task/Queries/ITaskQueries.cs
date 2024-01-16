using ToDoList.Domain.Entities;

namespace ToDoList.Application.Task.Queries
{
    public interface ITaskQueries
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(int page, int pageSize, CancellationToken cancellationToken);
    }
}