using FluentAssertions;
using ToDoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Models;

namespace ToDoList.Tests.Domain;

public class TaskItemTests
{
    [Fact]
    public void TaskItem_UpdateProperties_ShouldUpdateCorrectly()
    {
        // Arrange
        var originalTask = Todo.Create("Original Title", "Original Description", DateTime.Now, ETodoStatus.Pending);

        // Act
        originalTask.Update("Updated Title", "Updated Description", ETodoStatus.Pending);

        // Assert
        originalTask.Title.Should().Be("Updated Title");
        originalTask.Description.Should().Be("Updated Description");
        originalTask.Status.Should().Be(ETodoStatus.Pending);
    }

    [Fact]
    public void TaskItem_UpdateProperties_WithEmptyValues_ShouldNotUpdate()
    {
        // Arrange
        var originalTask = Todo.Create("Original Title", "Original Description", DateTime.Now, ETodoStatus.Pending);

        // Act
        originalTask.Update("", "", ETodoStatus.Pending);

        // Assert
        originalTask.Title.Should().Be("Original Title");
        originalTask.Description.Should().Be("Original Description");
        originalTask.Status.Should().Be(ETodoStatus.Pending);
    }
}