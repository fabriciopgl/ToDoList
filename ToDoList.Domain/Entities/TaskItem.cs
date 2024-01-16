using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Domain.Models;

namespace ToDoList.Domain.Entities;

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
        Title = title;
        Description = description;
        Status = status;
    }
}
