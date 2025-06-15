using TodoList.WebApi.Handlers;

namespace TodoList.WebApi.DependencyInjection;

public static class ExceptionHandlerInjection
{
    public static void AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
}