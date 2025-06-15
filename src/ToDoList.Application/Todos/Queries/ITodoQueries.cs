using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Queries;

public interface ITodoQueries
{
    Task<IEnumerable<Todo>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
}