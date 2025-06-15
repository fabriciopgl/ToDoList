using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoList.Application.Users.Domain;
using TodoList.Application.Users.Services;
using TodoList.Infraestructure.Repositories;
using TodoList.Infraestructure.Services;

namespace TodoList.WebApi.DependencyInjection;

public static class AuthenticationInjection
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT Configuration
        var jwtSettings = configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey not found");
        var issuer = jwtSettings["Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer not found");
        var audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("Jwt:Audience not found");

        var key = Encoding.UTF8.GetBytes(secretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false; // Para desenvolvimento
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        // Auth Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordService, PasswordService>();
    }
}