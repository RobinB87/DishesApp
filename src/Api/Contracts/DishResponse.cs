namespace Api.Contracts;

public class DishResponse
{
    public int DishId { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Recipe { get; set; }
    public required List<DishIngredientResponse> Ingredients { get; set; }
}
