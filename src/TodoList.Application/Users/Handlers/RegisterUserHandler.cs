using CSharpFunctionalExtensions;
using MediatR;
using TodoList.Application.Users.Commands;
using TodoList.Application.Users.Services;

namespace TodoList.Application.Users.Handlers;

public class RegisterUserHandler(IAuthService authService) : IRequestHandler<RegisterUserCommand, Result<int>>
{
    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await authService.RegisterAsync(request.Name, request.Email, request.Password, cancellationToken);
    }
}
