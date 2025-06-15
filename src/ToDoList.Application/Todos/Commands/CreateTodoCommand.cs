using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Todos.Models;

namespace ToDoList.Application.Todos.Commands;

public record CreateTodoCommand(string Title, string Description, DateTime DueDate, ETodoStatus Status) : IRequest<Result<int>>;