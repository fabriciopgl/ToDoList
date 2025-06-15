using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Todos.Models;

namespace ToDoList.Application.Todos.Commands;

public record UpdateTodoCommand(int Id, string Title, string Description, ETodoStatus Status) : IRequest<Result>;