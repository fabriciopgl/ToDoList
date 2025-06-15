using AspNetCoreRateLimit;

namespace TodoList.WebApi.DependencyInjection;

public static class RateLimitingInjection
{
    public static void AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

        services.AddHttpContextAccessor();
    }
}