using CSharpFunctionalExtensions;
using MediatR;

namespace TodoList.Application.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<Result>;
