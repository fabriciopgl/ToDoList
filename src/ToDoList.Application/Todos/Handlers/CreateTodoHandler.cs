using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Todos.Commands;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Handlers;

public class CreateTodoHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        Todo todo = Todo.Create(request.Title, request.Description, request.DueDate, request.Status);

        var newTask = await todoRepository.Add(todo, cancellationToken);

        return Result.Success(newTask.Id);
    }
}
