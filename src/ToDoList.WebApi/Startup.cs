using TodoList.WebApi.DependencyInjection;
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
        services.AddJwtAuthentication(Configuration);
        services.AddGlobalExceptionHandler();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        // Authentication & Authorization middleware
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