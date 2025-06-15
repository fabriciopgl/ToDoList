using CSharpFunctionalExtensions;
using Moq;
using ToDoList.Application.Todos.Commands;
using ToDoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Handlers;
using ToDoList.Application.Todos.Models;

namespace ToDoList.Tests.Application.Tasks;

public class TasksCommandHandlerTest
{
    [Fact]
    public async Task Handle_CreateTaskCommand_Success()
    {
        // Arrange
        var mockRepository = new Mock<ITodoRepository>();
        var handler = new CreateTodoHandler(mockRepository.Object);

        var createTaskCommand = new CreateTodoCommand("Sample Task", "Sample description", DateTime.Now.AddDays(7), ETodoStatus.Created);

        var expectedId = 1;
        var expectedResult = Result.Success(expectedId);

        mockRepository.Setup(repo => repo.Add(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(Todo.Create(createTaskCommand.Title, createTaskCommand.Description, createTaskCommand.DueDate, createTaskCommand.Status));

        // Act
        var result = await handler.Handle(createTaskCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        mockRepository.Verify(repo => repo.Add(It.IsAny<Todo>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdateTaskCommand_Success()
    {
        // Arrange
        var mockRepository = new Mock<ITodoRepository>();
        var handler = new UpdateTodoHandler(mockRepository.Object);

        var updateTaskCommand = new UpdateTodoCommand(1, "Update task", "Update description", ETodoStatus.Pending);

        var expectedUpdateResult = Result.Success();

        mockRepository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETodoStatus>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(Result.Success(Todo.Create(updateTaskCommand.Title, updateTaskCommand.Description, DateTime.Now.AddDays(7), updateTaskCommand.Status)));

        // Act
        var result = await handler.Handle(updateTaskCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedUpdateResult, result);
        mockRepository.Verify(repo => repo.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETodoStatus>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeleteTaskCommand_Success()
    {
        // Arrange
        var mockRepository = new Mock<ITodoRepository>();
        var handler = new DeleteTodoHandler(mockRepository.Object);

        var deleteTaskCommand = new DeleteTodoCommand(1);

        var expectedDeleteResult = Result.Success();

        mockRepository.Setup(repo => repo.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await handler.Handle(deleteTaskCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedDeleteResult, result);
        mockRepository.Verify(repo => repo.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
