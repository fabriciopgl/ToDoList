using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Application.Tasks.Models;

namespace ToDoList.Application.Tasks.Domain;

[Table("Tasks")]
public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public ETaskStatus Status { get; private set; }

    public TaskItem(int id, string title, string description, DateTime dueDate, ETaskStatus status)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
    }

    public void Update(string title, string description, ETaskStatus status)
    {
        if (!string.IsNullOrEmpty(title) || !string.IsNullOrWhiteSpace(title))
            Title = title;

        if (!string.IsNullOrEmpty(description) || !string.IsNullOrWhiteSpace(description))
            Description = description;

        Status = status;
    }
}
