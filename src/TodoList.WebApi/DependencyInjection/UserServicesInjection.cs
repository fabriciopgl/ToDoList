using TodoList.Application.Users.Queries;

namespace TodoList.WebApi.DependencyInjection;

public static class UserServicesInjection
{
    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserQueries, UserQueries>();
    }
}
