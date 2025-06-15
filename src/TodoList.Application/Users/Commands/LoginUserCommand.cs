using CSharpFunctionalExtensions;
using MediatR;

namespace TodoList.Application.Users.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<string>>;
