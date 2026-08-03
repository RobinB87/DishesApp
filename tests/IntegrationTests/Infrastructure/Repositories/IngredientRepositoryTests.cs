using Domain.Entities;
using Infrastructure.Repositories;

namespace Tests.IntegrationTests.Infrastructure.Repositories;

public class IngredientRepositoryTests : PostgresIntegrationTestBase
{
    private IngredientRepository _repository = null!;
    private readonly IEnumerable<Ingredient> _seededIngredients =
    [
        new Ingredient("Tomato", 0.5),
        new Ingredient("Onion", 0.3),
        new Ingredient("Garlic", 0.2)
    ];

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _repository = new IngredientRepository(Context);
    }

    private async Task<Ingredient> SeedIngredientAsync(string name = "Tomato", double pricePerUnit = 0.5)
    {
        var ingredient = new Ingredient(name, pricePerUnit);
        return await _repository.AddAsync(ingredient);
    }

    [Fact]
    public async Task AddAsync_PersistsIngredient()
    {
        var actual = await SeedIngredientAsync();

        Assert.NotNull(actual);
        Assert.Equal(0.5, actual.PricePerUnit);
    }

    [Fact]
    public async Task AddAsync_PersistsMultipleIngredients()
    {
        var actual = await _repository.AddManyAsync(_seededIngredients);

        Assert.NotNull(actual);
        Assert.Equal(_seededIngredients.Count(), actual.Count());
        foreach (var ingredient in actual)
        {
            Assert.Contains(_seededIngredients, i => i.Name == ingredient.Name && i.PricePerUnit == ingredient.PricePerUnit);
        }
    }

    [Fact]
    public async Task AddAsync_FetchesAllIngredients()
    {
        await SeedIngredientAsync();

        var actual = await _repository.GetAllAsync();

        Assert.NotNull(actual);
        Assert.Single(actual);
    }

    [Fact]
    public async Task AddAsync_FetchesOneIngredient()
    {
        var added = await SeedIngredientAsync();

        var actual = await _repository.GetByIdAsync(added.Id);

        Assert.NotNull(actual);
        Assert.Equal(0.5, actual.PricePerUnit);
    }

    [Fact]
    public async Task AddAsync_FetchesIngredientsByName()
    {
        await _repository.AddManyAsync(_seededIngredients);
        var actual = await _repository.GetByNamesAsync(_seededIngredients.Select(i => i.Name));

        Assert.NotNull(actual);
        foreach (var ingredient in actual)
        {
            Assert.Contains(_seededIngredients, i => i.Name == ingredient.Name && i.PricePerUnit == ingredient.PricePerUnit);
        }
    }

    [Fact]
    public async Task AddAsync_ThrowsDbUpdateException_WhenNameAlreadyExists()
    {
        var name = "Tomato";
        await SeedIngredientAsync(name);

        await Assert.ThrowsAsync<InvalidOperationException>(() => SeedIngredientAsync(name));
    }
}
