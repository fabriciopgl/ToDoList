using CSharpFunctionalExtensions;
using MediatR;

namespace ToDoList.Application.Task.Commands;

public class DeleteTaskCommand(int id) : IRequest<Result>
{
    public int Id { get; set; } = id;
}
