using TodoList.WebApi.DependencyInjection;
using ToDoList.WebApi.DependencyInjection;

namespace ToDoList.WebApi;

public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
{
    public IConfiguration Configuration { get; } = configuration;
    public IWebHostEnvironment Environment { get; } = environment;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddQueries();
        services.AddMediatR();
        services.AddVersioning();
        services.AddControllers();
        services.AddRepositories();
        services.AddHealthChecks();
        services.AddUserServices();
        services.AddDatabase(Configuration);
        services.AddGlobalExceptionHandler();
        services.AddRateLimiting(Configuration);
        services.AddHttpsConfiguration(Environment);
        services.AddJwtAuthentication(Configuration);
        services.AddCorsConfiguration(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
        ApplicationBuilderInjection.Configure(app, env);
}