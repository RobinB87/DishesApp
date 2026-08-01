using Api.Contracts;
using Domain.Entities;

namespace Api.Mapping;

public static class DishExtensions
{
    public static DishResponse ToResponse(this Dish dish)
    {
        return new DishResponse
        {
            DishId = dish.Id,
            Name = dish.Name,
            Country = dish.Country,
            Recipe = dish.Recipe,
            Ingredients = dish.DishIngredients.Select(di => di.ToResponse()).ToList()
        };
    }

    public static DishIngredientResponse ToResponse(this DishIngredient dishIngredient)
    {
        return new DishIngredientResponse
        {
            IngredientName = dishIngredient.Ingredient.Name,
            Quantity = dishIngredient.Quantity,
            PricePerUnit = dishIngredient.Ingredient.PricePerUnit
        };
    }
}
