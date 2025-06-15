using CSharpFunctionalExtensions;

namespace TodoList.Application.Users.Services;

public interface IAuthService
{
    Task<Result<int>> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken);
    Task<Result<string>> LoginAsync(string email, string password, CancellationToken cancellationToken);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}
