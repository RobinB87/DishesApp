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

    private AppDbContext _context = null!;
    private IngredientRepository _repository = null!;
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

        _context = CreateContext();
        await _context.Database.EnsureCreatedAsync();

        _repository = new IngredientRepository(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_PersistsIngredient()
    {
        var ingredient = new Ingredient("Tomato", 0.5);

        var actual = await _repository.AddAsync(ingredient);

        Assert.NotNull(actual);
        Assert.Equal(0.5, actual.PricePerUnit);
    }

    [Fact]
    public async Task AddAsync_FetchesAllIngredients()
    {
        var ingredient = new Ingredient("Tomato", 0.5);

        await _repository.AddAsync(ingredient);
        var actual = await _repository.GetAllAsync();

        Assert.NotNull(actual);
        Assert.Single(actual);
    }

    [Fact]
    public async Task AddAsync_FetchesOneIngredient()
    {
        var ingredient = new Ingredient("Tomato", 0.5);

        var added = await _repository.AddAsync(ingredient);
        var actual = await _repository.GetByIdAsync(added.Id);

        Assert.NotNull(actual);
        Assert.Equal(0.5, actual.PricePerUnit);
    }

    [Fact]
    public async Task AddAsync_FetchesOneIngredientByName()
    {
        var expected = "Tomato";
        var ingredient = new Ingredient("Tomato", 0.5);

        await _repository.AddAsync(ingredient);
        var actual = await _repository.GetByNameAsync(expected);

        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Name);
    }
}
