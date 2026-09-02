using Api.Options;
using Microsoft.Extensions.Options;
using RateLimiting;
using StackExchange.Redis;

namespace Api.RateLimiting;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddRedisClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<RedisOptions>(configuration.GetSection(RedisOptions.SectionName));
        services.ConfigureOptions<RateLimitingOptions>(configuration.GetSection(RateLimitingOptions.SectionName));

        var redisOptions = configuration.GetSection(RedisOptions.SectionName).Get<RedisOptions>()
            ?? new RedisOptions();

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { { redisOptions.Host, redisOptions.Port } },
                User = redisOptions.Username,
                Password = redisOptions.Password,
            };

            return ConnectionMultiplexer.Connect(config);
        });

        services.AddSingleton<FixedWindowRateLimiter>();

        return services;
    }

    public static IApplicationBuilder UseRedisRateLimiting<TLimiter>(this IApplicationBuilder app) 
        where TLimiter : class, IRateLimiter
    {
        return app.Use(async (context, next) =>
        {
            var rateLimiter = context.RequestServices.GetRequiredService<TLimiter>();
            var clientKey = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var rateLimiterOptions = context.RequestServices
                .GetRequiredService<IOptions<RateLimitingOptions>>().Value;

            if (!await rateLimiter.IsAllowedAsync(clientKey))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.Headers.RetryAfter = 
                    rateLimiterOptions.Window.TotalSeconds.ToString();
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }
            
            await next();
        });
    }
}
