using System.Diagnostics;
using Api.Contracts;
using Api.Exceptions;
using Api.Options;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();
        services.AddExceptionHandler<ApiExceptionHandler>();
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.TraceId;
                ctx.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
            };
        });

        services.AddScoped<IValidator<CreateDishRequest>, CreateDishRequestValidator>();

        return services;
    }

    public static IServiceCollection AddApiLogging(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureOptions<SeqOptions>(configuration.GetSection(SeqOptions.SectionName));

        var seqOptions = configuration.GetSection(SeqOptions.SectionName).Get<SeqOptions>() ?? new SeqOptions();

        return services.AddSerilog((_, config) => config
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq(seqOptions.Url));
    }

    public static WebApplication MapApiHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready")
        });

        return app;
    }
}
