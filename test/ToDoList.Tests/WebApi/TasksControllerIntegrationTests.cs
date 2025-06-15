using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using ToDoList.Application.Todos.Models;
using ToDoList.WebApi;

namespace ToDoList.Tests.WebApi;

public class TasksControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public TasksControllerIntegrationTests(WebApplicationFactory<ToDoList.WebApi.Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetTasksAsync_ReturnsOkResult()
    {
        var client = _factory.CreateClient();

        // Setando os parâmetros da query string
        var page = 1;
        var pageSize = 5;

        // Construindo a URL com os parâmetros
        var url = $"/v1/Tasks?page={page}&pageSize={pageSize}";

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetTaskById_ReturnsOkResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Assuming there is a task with id = 1 in the database
        var taskId = 1;

        // Act
        var response = await client.GetAsync($"/v1/Tasks/{taskId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task PostTask_ReturnsCreatedResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        var createTaskCommand = new CreateTaskCommand("New Task", "Task Description", DateTime.Now.AddDays(7), ETodoStatus.Pending);

        var jsonContent = new StringContent(JsonConvert.SerializeObject(createTaskCommand), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/v1/Tasks", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}
