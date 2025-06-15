using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Application.Todos.Models.Enums;

namespace ToDoList.Application.Todos.Domain;

[Table("Todos")]
public class Todo
{
    public int Id { get; init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; init; }
    public ETodoStatus Status { get; private set; }
    public int UserId { get; init; }

    private Todo(int id, string title, string description, DateTime dueDate, ETodoStatus status, int userId)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
        UserId = userId;
    }

    public static Todo Create(string title, string description, DateTime dueDate, ETodoStatus status, int userId) =>
        new(0, title, description, dueDate, status, userId);

    public void Update(string title, string description, ETodoStatus status)
    {
        if (!string.IsNullOrEmpty(title) || !string.IsNullOrWhiteSpace(title))
            Title = title;

        if (!string.IsNullOrEmpty(description) || !string.IsNullOrWhiteSpace(description))
            Description = description;

        Status = status;
    }
}
