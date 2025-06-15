using ToDoList.WebApi.DependencyInjection;
using ToDoList.WebApi.Extensions;

namespace ToDoList.WebApi;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddQueries();
        services.AddMediatR();
        services.AddVersioning();
        services.AddControllers();
        services.AddRepositories();
        services.AddHealthChecks();
        services.AddDatabase(Configuration);
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