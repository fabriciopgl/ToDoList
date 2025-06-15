using CSharpFunctionalExtensions;
using MediatR;

namespace ToDoList.Application.Todos.Commands;

public record DeleteTodoCommand(int Id) : IRequest<Result>;
