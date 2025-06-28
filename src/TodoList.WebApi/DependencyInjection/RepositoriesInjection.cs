using ToDoList.Application.Todos.Domain;
using ToDoList.Infraestructure.Repositories;

namespace ToDoList.WebApi.DependencyInjection;

public static class RepositoriesInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, TodoRepository>();
    }
}
