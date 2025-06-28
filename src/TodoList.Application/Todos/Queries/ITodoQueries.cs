using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Queries;

public interface ITodoQueries
{
    Task<Todo?> GetById(int id, CancellationToken cancellationToken);
    Task<Todo?> GetByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task<IEnumerable<Todo>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<IEnumerable<Todo>> GetByUserIdAsync(int userId, int page, int pageSize, CancellationToken cancellationToken);
}