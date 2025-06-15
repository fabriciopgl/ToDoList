using ToDoList.WebApi.Middlewares;

namespace ToDoList.WebApi.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}