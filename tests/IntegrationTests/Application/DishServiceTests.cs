using Api.Contracts;
using Application.Dishes;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Tests.IntegrationTests.Application;

public class DishServiceTests : PostgresIntegrationTestBase
{
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

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var _dishRepository = new DishRepository(Context);
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
        await _ingredientRepository.AddAsync(new Ingredient("Tomato", 0.5));

        var actual = await _dishService.AddAsync(_createDishRequest);

        var allIngredients = await _ingredientRepository.GetAllAsync();
        Assert.Equal(2, allIngredients.Count());
        Assert.Single(allIngredients, i => i.Name == "Tomato");
    }
}
