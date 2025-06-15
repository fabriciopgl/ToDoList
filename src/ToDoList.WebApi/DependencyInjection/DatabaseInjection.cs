using Microsoft.EntityFrameworkCore;
using TodoList.Application.Todos.Domain;
using TodoList.Core.Context;
using TodoList.Infraestructure.Context;

namespace ToDoList.WebApi.DependencyInjection;

public static class DatabaseInjection
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Todo")));

        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<TodoDbContext>());

        services.AddScoped<ITodoDbContext>(provider =>
            provider.GetRequiredService<TodoDbContext>());
    }
}
