using Serilog;

namespace ToDoList.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .Console()
            .CreateBootstrapLogger();

        Log.Information("Starting application");

        try
        {
            CreateHostBuilder(args).Build().Run();
            Log.Information("Application finished successfully");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Error to open the application");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

}