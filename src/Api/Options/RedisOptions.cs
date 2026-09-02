using System.ComponentModel.DataAnnotations;

namespace Api.Options;

public class RedisOptions
{
    public const string SectionName = "Redis";

    [Required]
    public string Host { get; set; } = string.Empty;

    [Required]
    public int Port { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
