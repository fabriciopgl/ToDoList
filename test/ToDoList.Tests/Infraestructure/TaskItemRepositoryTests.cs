using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Tasks.Domain;
using ToDoList.Application.Tasks.Models;
using ToDoList.Infraestructure.Context;
using ToDoList.Infraestructure.Repositories;

namespace ToDoList.Tests.Infraestructure;

public class TaskItemRepositoryTests
{
    [Fact]
    public async Task GetTaskByIdAsync_ExistingId_ShouldReturnTask()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        var existingTask = new TaskItem(2587, "Existing Task", "Existing Description", DateTime.Now, ETaskStatus.Pending);
        dbContext.TaskItems.Add(existingTask);
        await dbContext.SaveChangesAsync();

        // Act
        var retrievedTask = await repository.GetTaskByIdAsync(existingTask.Id, CancellationToken.None);

        // Assert
        retrievedTask.Should().NotBeNull();
        retrievedTask!.Id.Should().Be(existingTask.Id);
        retrievedTask!.Title.Should().Be("Existing Task");
    }

    [Fact]
    public async Task GetTaskByIdAsync_NonExistingId_ShouldReturnNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        // Act
        var retrievedTask = await repository.GetTaskByIdAsync(999, CancellationToken.None);

        // Assert
        retrievedTask.Should().BeNull();
    }

    [Fact]
    public async Task AddTask_ShouldAddTaskToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        var taskToAdd = new TaskItem(8742, "Test Task", "Test Description", DateTime.Now, ETaskStatus.Pending);

        // Act
        var addedTask = await repository.AddTask(taskToAdd, CancellationToken.None);

        // Assert
        addedTask.Should().NotBeNull();
        addedTask.Id.Should().NotBe(0);

        var taskFromDatabase = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == addedTask.Id);
        taskFromDatabase.Should().NotBeNull();
        taskFromDatabase!.Title.Should().Be("Test Task");
    }

    [Fact]
    public async Task UpdateTask_ExistingId_ShouldUpdateTask()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        var existingTask = new TaskItem(9871, "Existing Task", "Existing Description", DateTime.Now, ETaskStatus.Pending);
        dbContext.TaskItems.Add(existingTask);
        await dbContext.SaveChangesAsync();

        // Act
        var updateResult = await repository.UpdateTask(existingTask.Id, "Updated Task", "Updated Description", ETaskStatus.Pending, CancellationToken.None);

        // Assert
        updateResult.IsSuccess.Should().BeTrue();

        var updatedTask = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == existingTask.Id);
        updatedTask.Should().NotBeNull();
        updatedTask!.Title.Should().Be("Updated Task");
        updatedTask!.Description.Should().Be("Updated Description");
        updatedTask!.Status.Should().Be(ETaskStatus.Pending);
    }

    [Fact]
    public async Task UpdateTask_NonExistingId_ShouldReturnFailure()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        // Act
        var updateResult = await repository.UpdateTask(999, "Updated Task", "Updated Description", ETaskStatus.Pending, CancellationToken.None);

        // Assert
        updateResult.IsFailure.Should().BeTrue();
        updateResult.Error.Should().Be("Fail to find Task to update");
    }

    [Fact]
    public async Task DeleteTask_ExistingId_ShouldDeleteTask()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        var existingTask = new TaskItem(7851, "Existing Task", "Existing Description", DateTime.Now, ETaskStatus.Pending);
        dbContext.TaskItems.Add(existingTask);
        await dbContext.SaveChangesAsync();

        // Act
        var deleteResult = await repository.DeleteTask(existingTask.Id, CancellationToken.None);

        // Assert
        deleteResult.IsSuccess.Should().BeTrue();

        var deletedTask = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == existingTask.Id);
        deletedTask.Should().BeNull();
    }

    [Fact]
    public async Task DeleteTask_NonExistingId_ShouldReturnFailure()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TaskItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TaskItemDbContext(options);
        var repository = new TaskItemRepository(dbContext);

        // Act
        var deleteResult = await repository.DeleteTask(999, CancellationToken.None);

        // Assert
        deleteResult.IsFailure.Should().BeTrue();
        deleteResult.Error.Should().Be("Cannot find Task to delete");
    }
}
