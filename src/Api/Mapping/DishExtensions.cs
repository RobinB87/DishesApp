using Api.Contracts;
using Api.Contracts.V2;
using Domain.Entities;

namespace Api.Mapping;

public static class DishExtensions
{
    public static DishResponseBase ToBaseResponse(this Dish dish)
    {
        return new DishResponseBase
        {
            DishId = dish.Id,
            Name = dish.Name,
            Country = dish.Country,
            Recipe = dish.Recipe
        };
    }

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

    public static DishResponseBaseV2 ToBaseResponseV2(this Dish dish)
    {
        return new DishResponseBaseV2
        {
            Guid = dish.Guid,
            Name = dish.Name,
            Country = dish.Country,
            Recipe = dish.Recipe
        };
    }

    public static DishResponseV2 ToResponseV2(this Dish dish)
    {
        return new DishResponseV2
        {
            Guid = dish.Guid,
            Name = dish.Name,
            Country = dish.Country,
            Recipe = dish.Recipe,
            Ingredients = dish.DishIngredients.Select(di => di.ToResponse()).ToList()
        };
    }
}
