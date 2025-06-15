using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace TodoList.WebApi.Middlewares;

public class JsonRateLimitMiddleware(
    RequestDelegate next,
    IProcessingStrategy processingStrategy,
    IOptions<IpRateLimitOptions> options,
    IIpPolicyStore policyStore,
    IRateLimitConfiguration config,
    ILogger<IpRateLimitMiddleware> logger) : IpRateLimitMiddleware(next, processingStrategy, options, policyStore, config, logger)
{

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
    {
        var response = new
        {
            error = "Rate limit exceeded",
            message = $"Too many requests. Limit: {rule.Limit} per {rule.Period}",
            retryAfter,
            endpoint = rule.Endpoint,
            timestamp = DateTime.UtcNow
        };

        httpContext.Response.Headers.Append("Retry-After", retryAfter);
        httpContext.Response.StatusCode = 429;
        httpContext.Response.ContentType = "application/json";

        var jsonResponse = JsonSerializer.Serialize(response, _jsonSerializerOptions);

        return httpContext.Response.WriteAsync(jsonResponse);
    }
}
