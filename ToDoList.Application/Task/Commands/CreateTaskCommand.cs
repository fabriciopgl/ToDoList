using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Domain.Models;

namespace ToDoList.Application.Task.Commands;

public class CreateTaskCommand(string title, string description, DateTime dueDate, ETaskStatus status) : IRequest<Result<int>>
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public DateTime DueDate { get; set; } = dueDate;
    public ETaskStatus Status { get; set; } = status;
}