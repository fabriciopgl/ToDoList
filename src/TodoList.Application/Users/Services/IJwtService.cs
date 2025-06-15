using TodoList.Application.Users.Domain;

namespace TodoList.Application.Users.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    int? GetUserIdFromToken(string token);
}