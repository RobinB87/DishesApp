using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Tests.IntegrationTests;

public abstract class PostgresIntegrationTestBase : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:18.4-alpine3.24")
          .WithDatabase("dishes")
          .Build();

    protected AppDbContext Context { get; private set; } = null!;

    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .Options;
        return new AppDbContext(options);
    }

    public virtual async Task InitializeAsync()
    {
        await _postgres.StartAsync();

        Context = CreateContext();
        await Context.Database.EnsureCreatedAsync();
    }

    public virtual async Task DisposeAsync()
    {
        await Context.DisposeAsync();
        await _postgres.DisposeAsync();
    }
}
