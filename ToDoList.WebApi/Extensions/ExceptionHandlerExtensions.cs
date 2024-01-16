using Serilog;
using System.Net;
using System.Text.Json;

namespace ToDoList.WebApi.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var logger = Log.ForContext<ExceptionHandlerMiddleware>();

        logger.Error(exception, "An error has occurred");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new
        {
            type = exception.GetType().Name,
            message = exception.Message,
            stackTrace = exception.StackTrace,
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}