namespace RateLimiting;

public interface IRateLimiter
{
    Task<bool> IsAllowedAsync(string key);
}