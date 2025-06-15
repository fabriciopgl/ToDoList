using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Todos.Commands;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Handlers;

public class DeleteTodoHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, Result>
{
    public async Task<Result> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var result = await todoRepository.Delete(request.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();
    }
}