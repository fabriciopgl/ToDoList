using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebApi.DependencyInjection;

public static class ApiVersioningInjection
{
    public static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
    }
}
