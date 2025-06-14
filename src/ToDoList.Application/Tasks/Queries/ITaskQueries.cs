using ToDoList.Application.Tasks.Domain;

namespace ToDoList.Application.Tasks.Queries
{
    public interface ITaskQueries
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(int page, int pageSize, CancellationToken cancellationToken);
    }
}