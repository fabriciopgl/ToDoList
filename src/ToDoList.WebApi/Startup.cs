using AspNetCoreRateLimit;
using TodoList.WebApi.DependencyInjection;
using TodoList.WebApi.Middlewares;
using ToDoList.WebApi.DependencyInjection;

namespace ToDoList.WebApi;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddQueries();
        services.AddUserServices();
        services.AddMediatR();
        services.AddVersioning();
        services.AddControllers();
        services.AddRepositories();
        services.AddHealthChecks();
        services.AddDatabase(Configuration);
        services.AddGlobalExceptionHandler();
        services.AddRateLimiting(Configuration);
        services.AddJwtAuthentication(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseMiddleware<JsonRateLimitMiddleware>();

        app.UseIpRateLimiting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseExceptionHandler();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}