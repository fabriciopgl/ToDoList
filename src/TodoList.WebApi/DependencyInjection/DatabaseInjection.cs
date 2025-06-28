using Microsoft.EntityFrameworkCore;
using TodoList.Application.Todos.Domain;
using TodoList.Application.Users.Domain;
using TodoList.Core.Context;
using TodoList.Infraestructure.Context;

namespace ToDoList.WebApi.DependencyInjection;

public static class DatabaseInjection
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Todo")));

        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ITodoDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUserDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
    }
}