namespace Api.Contracts.V2;

public class DishResponseV2 : DishResponseBaseV2
{
    public required List<DishIngredientResponse> Ingredients { get; set; }
}
