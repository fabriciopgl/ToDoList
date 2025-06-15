using TodoList.Application.Todos.Models.Enums;

namespace TodoList.Application.Todos.Models.InputModel;
public record CreateTodoRequest(string Title, string Description, DateTime DueDate, ETodoStatus Status);
