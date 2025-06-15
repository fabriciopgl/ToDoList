using CSharpFunctionalExtensions;
using MediatR;

namespace TodoList.Application.Users.Commands;
public record RegisterUserCommand(string Name, string Email, string Password) : IRequest<Result<int>>;
