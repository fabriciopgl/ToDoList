using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace ToDoList.WebApi.DependencyInjection;

public static class DocumentationInjection
{
    public static void AddDocumentation(this IServiceCollection services)
    {
        // .NET 9 Native OpenAPI support
        services.AddOpenApi("v1", options =>
        {
            options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;

            // Configure document info
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo List API",
                    Description = "A clean and modern API for managing todo tasks with user authentication",
                    Contact = new OpenApiContact
                    {
                        Name = "Development Team",
                        Email = "dev@todolist.com",
                        Url = new Uri("https://github.com/fabriciopgl/TodoList")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License"
                    }
                };

                // Add JWT Security Scheme
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token"
                };

                // Add security requirement
                document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                return Task.CompletedTask;
            });
        });
    }
}