using CSharpFunctionalExtensions;

namespace TodoList.Application.Users.Domain;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> Add(User user, CancellationToken cancellationToken);
    Task Update(CancellationToken cancellationToken);
    Task<Result> Delete(User user, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
}