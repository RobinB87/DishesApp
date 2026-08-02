using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Tests.IntegrationTests.Infrastructure.Repositories;

public class IngredientRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:18.4-alpine3.24")
          .WithDatabase("dishes")
          .Build();

    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .Options;
        return new AppDbContext(options);
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await _postgres.DisposeAsync();

    [Fact]
    public async Task AddAsync_PersistsIngredient()
    {
        await using var context = CreateContext();
        var repository = new IngredientRepository(context);
        var ingredient = new Ingredient("Tomato", 0.5);

        var result = await repository.AddAsync(ingredient);

        Assert.NotNull(result);
        Assert.Equal(0.5, result.PricePerUnit);
    }
}
