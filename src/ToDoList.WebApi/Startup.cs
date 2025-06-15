using TodoList.WebApi.DependencyInjection;
using TodoList.WebApi.Middlewares;
using ToDoList.WebApi.DependencyInjection;

namespace ToDoList.WebApi;

public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
{
    public IConfiguration Configuration { get; } = configuration;
    public IWebHostEnvironment Environment { get; } = environment;

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
        services.AddHttpsConfiguration(Environment);
        services.AddJwtAuthentication(Configuration);
        services.AddCorsConfiguration(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseCors("DevelopmentPolicy");
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseCors("ProductionPolicy");
        }

        app.Use((context, next) =>
        {
            context.Response.Headers.XContentTypeOptions = "nosniff";
            context.Response.Headers.XFrameOptions = "DENY";
            context.Response.Headers.XXSSProtection = "1; mode=block";

            return next();
        });

        app.UseRouting();

        app.UseMiddleware<JsonRateLimitMiddleware>();

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