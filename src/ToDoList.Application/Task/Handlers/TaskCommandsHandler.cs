using CSharpFunctionalExtensions;
using MediatR;
using ToDoList.Application.Task.Abstraction;
using ToDoList.Application.Task.Commands;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Task.Handlers;

public class TaskCommandsHandler : IRequestHandler<CreateTaskCommand, Result<int>>,
                                   IRequestHandler<UpdateTaskCommand, Result>,
                                   IRequestHandler<DeleteTaskCommand, Result>
{
    private readonly ITaskItemRepository _taskItemRepository;

    public TaskCommandsHandler(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        TaskItem task = new(0, request.Title, request.Description, request.DueDate, request.Status);

        var newTask = await _taskItemRepository.AddTask(task, cancellationToken);

        return Result.Success(newTask.Id);
    }

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await _taskItemRepository.UpdateTask(request.Id, request.Title, request.Description, request.Status, cancellationToken);
        if(updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        return Result.Success();
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _taskItemRepository.DeleteTask(request.Id, cancellationToken);
        if(result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();

    }
}

