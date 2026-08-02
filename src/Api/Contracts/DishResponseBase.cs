namespace Api.Contracts;

public class DishResponseBase
{
    public int DishId { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Recipe { get; set; }
}
