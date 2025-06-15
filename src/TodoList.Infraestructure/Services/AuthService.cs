using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Users.Domain;
using TodoList.Application.Users.Services;

namespace TodoList.Infraestructure.Services;

public class AuthService(IUserRepository userRepository, IJwtService jwtService, IPasswordService passwordService) : IAuthService
{
    public async Task<Result<int>> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken)
    {
        var emailExists = await userRepository.EmailExistsAsync(email, cancellationToken);
        if (emailExists)
            return Result.Failure<int>("Email already exists");

        var passwordHash = HashPassword(password);
        var user = User.Create(name, email, passwordHash);

        var createdUser = await userRepository.Add(user, cancellationToken);
        return Result.Success(createdUser.Id);
    }

    public async Task<Result<string>> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
            return Result.Failure<string>("Invalid email or password");

        if (!VerifyPassword(password, user.PasswordHash))
            return Result.Failure<string>("Invalid email or password");

        var token = jwtService.GenerateToken(user);
        return Result.Success(token);
    }

    public string HashPassword(string password)
    {
        return passwordService.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return passwordService.VerifyPassword(password, hash);
    }
}