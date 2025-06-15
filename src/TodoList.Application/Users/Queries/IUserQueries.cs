using TodoList.Application.Users.Domain;

namespace TodoList.Application.Users.Queries;

public interface IUserQueries
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
}