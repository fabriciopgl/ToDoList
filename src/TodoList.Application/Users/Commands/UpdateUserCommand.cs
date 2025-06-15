using CSharpFunctionalExtensions;
using MediatR;

namespace TodoList.Application.Users.Commands;

public record UpdateUserCommand(int Id, string Name, string Email) : IRequest<Result>;
