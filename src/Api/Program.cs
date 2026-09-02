using Api.RateLimiting;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiLogging(builder.Configuration);
builder.Services.AddRedisClient(builder.Configuration);

builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRedisRateLimiting<FixedWindowRateLimiter>();

app.MapControllers();

app.MapApiHealthChecks();

app.Run();
