using CSharpFunctionalExtensions;
using MediatR;
using TodoList.Application.Todos.Models.Enums;

namespace ToDoList.Application.Todos.Commands;

public record UpdateTodoCommand(int Id, string Title, string Description, ETodoStatus Status, int UserId) : IRequest<Result>;