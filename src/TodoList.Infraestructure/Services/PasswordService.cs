using Microsoft.AspNetCore.Identity;
using TodoList.Application.Users.Services;

namespace TodoList.Infraestructure.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<string> _passwordHasher;

    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<string>();
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(password, password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        var result = _passwordHasher.VerifyHashedPassword(password, hash, password);
        return result == PasswordVerificationResult.Success ||
               result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
