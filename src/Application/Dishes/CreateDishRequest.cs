namespace Api.Contracts;

public class CreateDishRequest
{
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Recipe { get; set; }
    public List<CreateDishIngredientRequest> Ingredients { get; set; } = [];
}
