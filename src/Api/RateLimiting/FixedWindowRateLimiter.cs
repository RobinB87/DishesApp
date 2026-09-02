using Api.Options;
using Microsoft.Extensions.Options;
using RateLimiting;
using StackExchange.Redis;

namespace Api.RateLimiting;

public class FixedWindowRateLimiter(
    IConnectionMultiplexer connectionMultiplexer,
    IOptions<RateLimitingOptions> options) : IRateLimiter
{
    private readonly IDatabase _redis = connectionMultiplexer.GetDatabase();
    private readonly int _limit = options.Value.Limit;
    private readonly TimeSpan _window = options.Value.Window;

    public async Task<bool> IsAllowedAsync(string key)
    {
        var redisKey = $"rate_limit:fixed:{key}";
        var currentCount = await _redis.StringIncrementAsync(redisKey);

        if (currentCount == 1)
        {
            await _redis.KeyExpireAsync(redisKey, _window);
        }

        if (currentCount > _limit)
        {
            return false;
        }

        return true;
    }
}