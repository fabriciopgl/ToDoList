using FluentAssertions;
using ToDoList.Application.Tasks.Domain;
using ToDoList.Application.Tasks.Models;

namespace ToDoList.Tests.Domain;

public class TaskItemTests
{
    [Fact]
    public void TaskItem_UpdateProperties_ShouldUpdateCorrectly()
    {
        // Arrange
        var originalTask = new TaskItem(1, "Original Title", "Original Description", DateTime.Now, ETaskStatus.Pending);

        // Act
        originalTask.Update("Updated Title", "Updated Description", ETaskStatus.Pending);

        // Assert
        originalTask.Title.Should().Be("Updated Title");
        originalTask.Description.Should().Be("Updated Description");
        originalTask.Status.Should().Be(ETaskStatus.Pending);
    }

    [Fact]
    public void TaskItem_UpdateProperties_WithEmptyValues_ShouldNotUpdate()
    {
        // Arrange
        var originalTask = new TaskItem(1, "Original Title", "Original Description", DateTime.Now, ETaskStatus.Pending);

        // Act
        originalTask.Update("", "", ETaskStatus.Pending);

        // Assert
        originalTask.Title.Should().Be("Original Title");
        originalTask.Description.Should().Be("Original Description");
        originalTask.Status.Should().Be(ETaskStatus.Pending);
    }
}