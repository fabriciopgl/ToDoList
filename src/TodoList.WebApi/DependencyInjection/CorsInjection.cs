namespace TodoList.WebApi.DependencyInjection;

public static class CorsInjection
{
    public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DevelopmentPolicy", builder =>
            {
                builder.WithOrigins(
                        "http://localhost:3000",      // React
                        "http://localhost:4200",      // Angular  
                        "http://localhost:5173"       // Vite
                    )
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                    .WithHeaders("Content-Type", "Authorization")
                    .AllowCredentials();
            });

            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["https://yourdomain.com"];

            options.AddPolicy("ProductionPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                       .WithMethods("GET", "POST", "PATCH", "DELETE")
                       .WithHeaders("Content-Type", "Authorization")
                       .AllowCredentials();
            });
        });
    }
}
