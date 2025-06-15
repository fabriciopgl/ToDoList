using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Todos.Commands;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Handlers;

public class UpdateTodoHandler(ITodoRepository todoRepository) : IRequestHandler<UpdateTodoCommand, Result>                                                                 
{
    public async Task<Result> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetTaskByIdAsync(request.Id, cancellationToken);
        if (todo == null)
            return Result.Failure<Todo>("Fail to find Todo to update");

        todo.Update(request.Title, request.Description, request.Status);

        await todoRepository.Update(cancellationToken);

        return Result.Success();
    }
}

