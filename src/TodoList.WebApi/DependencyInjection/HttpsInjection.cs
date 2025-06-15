namespace TodoList.WebApi.DependencyInjection;

public static class HttpsInjection
{
    public static void AddHttpsConfiguration(this IServiceCollection services, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });

            // HSTS apenas em produção
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
                options.ExcludedHosts.Clear();
            });
        }
    }
}
