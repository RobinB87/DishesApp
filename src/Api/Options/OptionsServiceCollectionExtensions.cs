namespace Api.Options;

public static class OptionsServiceCollectionExtensions
{
    public static IServiceCollection ConfigureOptions<T>(this IServiceCollection services,
        IConfigurationSection section) where T : class
    {
        services
            .AddOptions<T>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
