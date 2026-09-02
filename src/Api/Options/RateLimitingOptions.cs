namespace Api.Options;

public class RateLimitingOptions
{
    public const string SectionName = "RateLimiting";

    public int Limit { get; set; } = 10;
    public TimeSpan Window { get; set; } = TimeSpan.FromMinutes(1);
}