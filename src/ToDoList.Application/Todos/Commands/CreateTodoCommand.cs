using CSharpFunctionalExtensions;
using MediatR;
using TodoList.Application.Todos.Models.Enums;

namespace ToDoList.Application.Todos.Commands;

public record CreateTodoCommand(string Title, string Description, DateTime DueDate, ETodoStatus Status, int UserId) : IRequest<Result<int>>;