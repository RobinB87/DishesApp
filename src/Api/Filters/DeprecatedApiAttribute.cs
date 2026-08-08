using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class DeprecatedApiAttribute : ActionFilterAttribute
{
    public required string Sunset { get; init; }
    public required string SuccessorPath { get; init; }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var response = context.HttpContext.Response;
        response.Headers["Deprecation"] = "true";
        response.Headers["Sunset"] = Sunset;
        response.Headers["Link"] = $"<{SuccessorPath}>; rel=\"successor-version\"";

        base.OnResultExecuting(context);
    }
}
