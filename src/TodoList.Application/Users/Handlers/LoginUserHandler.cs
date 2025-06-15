using CSharpFunctionalExtensions;
using MediatR;
using TodoList.Application.Users.Commands;
using TodoList.Application.Users.Services;

namespace TodoList.Application.Users.Handlers;

public class LoginUserHandler(IAuthService authService) : IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request.Email, request.Password, cancellationToken);
    }
}
