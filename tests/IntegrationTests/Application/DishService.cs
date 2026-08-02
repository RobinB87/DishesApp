using Api.Contracts;
using Application.Dishes;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Tests.IntegrationTests.Application;

public class DishServiceTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:18.4-alpine3.24")
          .WithDatabase("dishes")
          .Build();

    private AppDbContext _context = null!;
    private DishService _dishService = null!;
    private IngredientRepository _ingredientRepository = null!;
    private CreateDishRequest _createDishRequest = new CreateDishRequest
    {
        Name = "Pasta",
        Country = "Italy",
        Recipe = "Boil pasta and add sauce.",
        Ingredients = new List<CreateDishIngredientRequest>
        {
            new CreateDishIngredientRequest { IngredientName = "Tomato", PricePerUnit = 0.5, Quantity = 2 },
            new CreateDishIngredientRequest { IngredientName = "Onion", PricePerUnit = 0.3, Quantity = 1 }
        }
    };

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

        var _dishRepository = new DishRepository(_context);
        _ingredientRepository = new IngredientRepository(_context);
        _dishService = new DishService(_dishRepository, _ingredientRepository);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_PersistsDish()
    {
        var actual = await _dishService.AddAsync(_createDishRequest);

        Assert.NotNull(actual);
        Assert.Equal(_createDishRequest.Name, actual.Name);
        Assert.Equal(_createDishRequest.Ingredients.First().IngredientName, actual.DishIngredients.First().Ingredient.Name);
    }

    [Fact]
    public async Task AddAsync_ReusesExistingIngredient()
    {
        await _ingredientRepository.AddAsync(new Ingredient("Tomato", 0.5));

        var actual = await _dishService.AddAsync(_createDishRequest);

        var allIngredients = await _ingredientRepository.GetAllAsync();
        Assert.Equal(2, allIngredients.Count());
        Assert.Single(allIngredients, i => i.Name == "Tomato");
    }
}
