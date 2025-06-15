using ToDoList.Application.Todos.Models;

namespace ToDoList.Tests.WebApi;
internal class CreateTaskCommand
{
    private string v1;
    private string v2;
    private DateTime dateTime;
    private ETodoStatus pending;

    public CreateTaskCommand(string v1, string v2, DateTime dateTime, ETodoStatus pending)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.dateTime = dateTime;
        this.pending = pending;
    }
}