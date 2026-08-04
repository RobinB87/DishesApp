using Api.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Api.Exceptions;

public class ApiExceptionHandler : IExceptionHandler
{
    private const string PostgresUniqueViolationSqlState = "23505";

    private readonly ILogger<ApiExceptionHandler> _logger;

    public ApiExceptionHandler(ILogger<ApiExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken ct)
    {
        var (statusCode, message) = exception switch
        {
            InvalidOperationException => (StatusCodes.Status409Conflict, exception.Message),
            DbUpdateException { InnerException: PostgresException { SqlState: PostgresUniqueViolationSqlState } postgresEx } =>
                (StatusCodes.Status409Conflict, BuildDuplicateKeyMessage(postgresEx)),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception");

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(ApiResult<object>.Fail(new ProblemDetails
        { 
            Title = "An error occurred", 
            Type = exception.GetType().Name, 
            Detail = message,
            Status = statusCode,
        }), ct);

        return true;
    }

    private static string BuildDuplicateKeyMessage(PostgresException postgresEx) =>
        postgresEx.TableName is { } tableName
            ? $"A duplicate value violates a unique constraint on '{tableName}'."
            : "A duplicate value violates a unique constraint.";
}
