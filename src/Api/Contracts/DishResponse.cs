namespace Api.Contracts;

public class DishResponse : DishResponseBase
{
    public required List<DishIngredientResponse> Ingredients { get; set; }
}
