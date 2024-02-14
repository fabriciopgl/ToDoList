using CSharpFunctionalExtensions;
using Moq;
using ToDoList.Application.Task.Abstraction;
using ToDoList.Application.Task.Commands;
using ToDoList.Application.Task.Handlers;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Models;

namespace ToDoList.Tests.Application.Tasks;

public class TasksCommandHandlerTest
{
    [Fact]
    public async Task Handle_CreateTaskCommand_Success()
    {
        // Arrange
        var mockRepository = new Mock<ITaskItemRepository>();
        var handler = new TaskCommandsHandler(mockRepository.Object);

        var createTaskCommand = new CreateTaskCommand("Sample Task", "Sample description", DateTime.Now.AddDays(7), ETaskStatus.Created);

        var expectedId = 1;
        var expectedResult = Result.Success(expectedId);

        mockRepository.Setup(repo => repo.AddTask(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new TaskItem(expectedId, createTaskCommand.Title, createTaskCommand.Description, createTaskCommand.DueDate, createTaskCommand.Status));

        // Act
        var result = await handler.Handle(createTaskCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        mockRepository.Verify(repo => repo.AddTask(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdateTaskCommand_Success()
    {
        // Arrange
        var mockRepository = new Mock<ITaskItemRepository>();
        var handler = new TaskCommandsHandler(mockRepository.Object);

        var updateTaskCommand = new UpdateTaskCommand(1, "Update task", "Update description", ETaskStatus.Pending);

        var expectedUpdateResult = Result.Success();

        mockRepository.Setup(repo => repo.UpdateTask(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETaskStatus>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(Result.Success(new TaskItem(updateTaskCommand.Id, updateTaskCommand.Title, updateTaskCommand.Description, DateTime.Now.AddDays(7), updateTaskCommand.Status)));

        // Act
        var result = await handler.Handle(updateTaskCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedUpdateResult, result);
        mockRepository.Verify(repo => repo.UpdateTask(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETaskStatus>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeleteTaskCommand_Success()
    {
        // Arrange
        var mockRepository = new Mock<ITaskItemRepository>();
        var handler = new TaskCommandsHandler(mockRepository.Object);

        var deleteTaskCommand = new DeleteTaskCommand(1);

        var expectedDeleteResult = Result.Success();

        mockRepository.Setup(repo => repo.DeleteTask(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await handler.Handle(deleteTaskCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedDeleteResult, result);
        mockRepository.Verify(repo => repo.DeleteTask(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
