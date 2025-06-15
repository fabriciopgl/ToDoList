using Microsoft.EntityFrameworkCore;
using ToDoList.Infraestructure.Context;

namespace ToDoList.WebApi.DependencyInjection;

public static class DatabaseInjection
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Todo")));
    }
}
