using Api.Contracts;
using Application.Repositories;
using Domain.Entities;

namespace Application.Dishes;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public DishService(IDishRepository dishRepository, IIngredientRepository ingredientRepository)
    {
        _dishRepository = dishRepository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<Dish> AddAsync(CreateDishRequest request)
    {
        var validator = new CreateDishRequestValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ArgumentException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var newIngredientNames = request.Ingredients.Select(i => i.IngredientName).ToList();
        var existingIngredients = await _ingredientRepository.GetByNamesAsync(newIngredientNames);
        
        var existingNamesHash = existingIngredients.Select(i => i.Name).ToHashSet();
        var newIngredients = new List<Ingredient>();
        foreach (var ingredientRequest in request.Ingredients)
        {
            if (!existingNamesHash.Contains(ingredientRequest.IngredientName))
                newIngredients.Add(new Ingredient(ingredientRequest.IngredientName, ingredientRequest.PricePerUnit));
        }

        await _ingredientRepository.AddManyAsync(newIngredients);

        var dish = new Dish(request.Name, request.Country, request.Recipe);
        var ingredientsByName = existingIngredients.Concat(newIngredients).ToDictionary(i => i.Name);
        foreach (var ingredientRequest in request.Ingredients)
        {
            var ingredient = ingredientsByName[ingredientRequest.IngredientName];
            dish.DishIngredients.Add(new DishIngredient(dish, ingredient, ingredientRequest.Quantity));
        }

        return await _dishRepository.AddAsync(dish);
    }

    public Task<IEnumerable<Dish>> GetAllAsync() =>
        _dishRepository.GetAllAsync();

    public Task<Dish?> GetByIdAsync(int id) =>
        _dishRepository.GetByIdAsync(id);
}
