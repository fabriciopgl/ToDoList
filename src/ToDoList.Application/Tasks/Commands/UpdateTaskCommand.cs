using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Tasks.Models;

namespace ToDoList.Application.Tasks.Commands;

public class UpdateTaskCommand(int id, string title, string description, ETaskStatus status) : IRequest<Result>
{
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public ETaskStatus Status { get; private set; } = status;
}