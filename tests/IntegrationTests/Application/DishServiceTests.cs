using Api.Contracts;
using Application.Dishes;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.IntegrationTests.Application;

public class DishServiceTests : PostgresIntegrationTestBase
{
    private DishService _dishService = null!;
    private DishRepository _dishRepository = null!;
    private IngredientRepository _ingredientRepository = null!;
    private CreateDishRequest _createDishRequest = new CreateDishRequest
    {
        Name = "Pasta",
        Country = "Italy",
        Recipe = "Boil pasta and add sauce.",
        Guid = Guid.NewGuid(),
        Ingredients = new List<CreateDishIngredientRequest>
        {
            new CreateDishIngredientRequest { IngredientName = "Tomato", PricePerUnit = 0.5, Quantity = 2, Guid = Guid.NewGuid() },
            new CreateDishIngredientRequest { IngredientName = "Onion", PricePerUnit = 0.3, Quantity = 1, Guid = Guid.NewGuid() }
        }
    };

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _dishRepository = new DishRepository(Context);
        _ingredientRepository = new IngredientRepository(Context);
        _dishService = new DishService(_dishRepository, _ingredientRepository);
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
        await _ingredientRepository.AddAsync(new Ingredient("Tomato", 0.5, Guid.NewGuid()));

        var actual = await _dishService.AddAsync(_createDishRequest);

        var allIngredients = await _ingredientRepository.GetAllAsync();
        Assert.Equal(2, allIngredients.Count());
        Assert.Single(allIngredients, i => i.Name == "Tomato");
    }

    [Fact]
    public async Task AddAsync_SameGuidAndName_IsIdempotent()
    {
        var first = await _dishService.AddAsync(_createDishRequest);
        var second = await _dishService.AddAsync(_createDishRequest);

        Assert.Equal(first.Id, second.Id);
        Assert.Equal(_createDishRequest.Ingredients.Count, second.DishIngredients.Count);
        var allDishes = await _dishRepository.GetAllAsync();
        Assert.Single(allDishes);
    }

    [Fact]
    public async Task AddAsync_SameGuidDifferentName_IsIdempotent_ReturnsOriginalDish()
    {
        var first = await _dishService.AddAsync(_createDishRequest);

        var retryWithDifferentName = new CreateDishRequest
        {
            Name = "A Completely Different Name",
            Country = _createDishRequest.Country,
            Recipe = _createDishRequest.Recipe,
            Guid = _createDishRequest.Guid,
            Ingredients = _createDishRequest.Ingredients
        };

        var second = await _dishService.AddAsync(retryWithDifferentName);

        Assert.Equal(first.Id, second.Id);
        Assert.Equal(_createDishRequest.Name, second.Name);
        var allDishes = await _dishRepository.GetAllAsync();
        Assert.Single(allDishes);
    }

    [Fact]
    public async Task AddAsync_ConcurrentRequestWithSameGuid_ThrowsUniqueConstraintViolation()
    {
        await _dishService.AddAsync(_createDishRequest);

        // Guid is now a real EF alternate key, so the context would otherwise catch the
        // duplicate in-memory (InvalidOperationException). Clearing the tracker forgets the
        // already-tracked dish without touching the DB, so the insert reaches Postgres and
        // hits the same unique constraint two independent requests' contexts would.
        Context.ChangeTracker.Clear();
        var duplicateDish = new Dish(_createDishRequest.Name, _createDishRequest.Country, _createDishRequest.Recipe, _createDishRequest.Guid);

        await Assert.ThrowsAsync<DbUpdateException>(() => _dishRepository.AddAsync(duplicateDish));
    }
}
