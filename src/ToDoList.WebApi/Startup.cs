using ToDoList.Application.Task.Abstraction;
using ToDoList.Infraestructure.Repositories;
using ToDoList.WebApi.DependencyInjection;
using ToDoList.WebApi.Extensions;

namespace ToDoList.WebApi;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabase(Configuration);
        services.AddVersioning();
        services.AddMediatR();
        services.AddQueries();
        services.AddRepositories();
        services.AddHealthChecks();
        services.AddControllers();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseGlobalExceptionHandler();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}