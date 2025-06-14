using ToDoList.Application.Tasks.Domain;
using ToDoList.Infraestructure.Repositories;

namespace ToDoList.WebApi.DependencyInjection;

public static class RepositoriesInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
    }
}
