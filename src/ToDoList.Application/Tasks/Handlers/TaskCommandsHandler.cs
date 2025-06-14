using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Tasks.Commands;
using ToDoList.Application.Tasks.Domain;

namespace ToDoList.Application.Tasks.Handlers;

public class TaskCommandsHandler(ITaskItemRepository taskItemRepository) : IRequestHandler<CreateTaskCommand, Result<int>>,
                                                                           IRequestHandler<UpdateTaskCommand, Result>,
                                                                           IRequestHandler<DeleteTaskCommand, Result>
{
    public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        TaskItem task = new(0, request.Title, request.Description, request.DueDate, request.Status);

        var newTask = await taskItemRepository.AddTask(task, cancellationToken);

        return Result.Success(newTask.Id);
    }

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await taskItemRepository.UpdateTask(request.Id, request.Title, request.Description, request.Status, cancellationToken);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        return Result.Success();
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await taskItemRepository.DeleteTask(request.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();

    }
}

