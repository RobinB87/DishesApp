using System.Diagnostics;
using Api.Contracts;
using Api.Exceptions;
using FluentValidation;
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
        IConfiguration configuration) =>
        services.AddSerilog((_, config) => config
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq(configuration["Seq:Url"] ?? "http://localhost:5341"));
}
