using Domain.Entities;
using Infrastructure.Repositories;

namespace Tests.IntegrationTests.Infrastructure.Repositories;

public class DishRepositoryTests : PostgresIntegrationTestBase
{
    private DishRepository _repository = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _repository = new DishRepository(Context);
    }

    private async Task<Dish> SeedDishAsync(string name = "Pizza", string country = "Italy", string recipe = "Bake it")
    {
        var dish = new Dish(name, country, recipe);
        return await _repository.AddAsync(dish);
    }

    [Fact]
    public async Task Exists_ReturnsTrue_WhenDishWithNameExists()
    {
        await SeedDishAsync("Pizza");

        var actual = await _repository.Exists("Pizza");

        Assert.True(actual);
    }

    [Fact]
    public async Task Exists_ReturnsFalse_WhenDishWithNameDoesNotExist()
    {
        await SeedDishAsync("Pizza");

        var actual = await _repository.Exists("Lasagna");

        Assert.False(actual);
    }
}
