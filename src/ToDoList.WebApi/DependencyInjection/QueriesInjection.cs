using ToDoList.Application.Tasks.Queries;

namespace ToDoList.WebApi.DependencyInjection;

public static class QueriesInjection
{
    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ITaskQueries, TaskQueries>();
    }
}
