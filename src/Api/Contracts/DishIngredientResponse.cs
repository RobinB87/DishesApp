namespace Api.Contracts;

public class DishIngredientResponse
{
    public required string IngredientName { get; set; }
    public double Quantity { get; set; }
    public double PricePerUnit { get; set; }
}
