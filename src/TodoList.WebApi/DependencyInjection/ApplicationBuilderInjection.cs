using TodoList.WebApi.Middlewares;

namespace TodoList.WebApi.DependencyInjection;

public static class ApplicationBuilderInjection
{
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
