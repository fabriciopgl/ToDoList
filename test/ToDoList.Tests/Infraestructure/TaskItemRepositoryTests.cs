using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Models;
using ToDoList.Infraestructure.Context;
using ToDoList.Infraestructure.Repositories;

namespace ToDoList.Tests.Infraestructure;

public class TaskItemRepositoryTests
{
    [Fact]
    public async Task GetTaskByIdAsync_ExistingId_ShouldReturnTask()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        var existingTask = Todo.Create("Existing Task", "Existing Description", DateTime.Now, ETodoStatus.Pending);
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
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        // Act
        var retrievedTask = await repository.GetTaskByIdAsync(999, CancellationToken.None);

        // Assert
        retrievedTask.Should().BeNull();
    }

    [Fact]
    public async Task AddTask_ShouldAddTaskToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        var taskToAdd = Todo.Create("Test Task", "Test Description", DateTime.Now, ETodoStatus.Pending);

        // Act
        var addedTask = await repository.Add(taskToAdd, CancellationToken.None);

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
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        var existingTask = Todo.Create("Existing Task", "Existing Description", DateTime.Now, ETodoStatus.Pending);
        dbContext.TaskItems.Add(existingTask);
        await dbContext.SaveChangesAsync();

        // Act
        var updateResult = await repository.Update(existingTask.Id, "Updated Task", "Updated Description", ETodoStatus.Pending, CancellationToken.None);

        // Assert
        updateResult.IsSuccess.Should().BeTrue();

        var updatedTask = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == existingTask.Id);
        updatedTask.Should().NotBeNull();
        updatedTask!.Title.Should().Be("Updated Task");
        updatedTask!.Description.Should().Be("Updated Description");
        updatedTask!.Status.Should().Be(ETodoStatus.Pending);
    }

    [Fact]
    public async Task UpdateTask_NonExistingId_ShouldReturnFailure()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        // Act
        var updateResult = await repository.Update(999, "Updated Task", "Updated Description", ETodoStatus.Pending, CancellationToken.None);

        // Assert
        updateResult.IsFailure.Should().BeTrue();
        updateResult.Error.Should().Be("Fail to find Task to update");
    }

    [Fact]
    public async Task DeleteTask_ExistingId_ShouldDeleteTask()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        var existingTask = Todo.Create("Existing Task", "Existing Description", DateTime.Now, ETodoStatus.Pending);
        dbContext.TaskItems.Add(existingTask);
        await dbContext.SaveChangesAsync();

        // Act
        var deleteResult = await repository.Delete(existingTask.Id, CancellationToken.None);

        // Assert
        deleteResult.IsSuccess.Should().BeTrue();

        var deletedTask = await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == existingTask.Id);
        deletedTask.Should().BeNull();
    }

    [Fact]
    public async Task DeleteTask_NonExistingId_ShouldReturnFailure()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new TodoDbContext(options);
        var repository = new TodoRepository(dbContext);

        // Act
        var deleteResult = await repository.Delete(999, CancellationToken.None);

        // Assert
        deleteResult.IsFailure.Should().BeTrue();
        deleteResult.Error.Should().Be("Cannot find Task to delete");
    }
}
