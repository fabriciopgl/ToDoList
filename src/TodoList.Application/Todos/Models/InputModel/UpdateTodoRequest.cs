using TodoList.Application.Todos.Models.Enums;

namespace TodoList.Application.Todos.Models.InputModel;

public record UpdateTodoRequest(string Title, string Description, ETodoStatus Status);
