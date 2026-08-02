namespace Api.Contracts;

public class CreateDishIngredientRequest
{
    public required string IngredientName { get; set; }
    public double PricePerUnit { get; set; }
    public double Quantity { get; set; }
}
