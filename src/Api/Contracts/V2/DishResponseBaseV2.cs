namespace Api.Contracts.V2;

public class DishResponseBaseV2
{
    public Guid Guid { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Recipe { get; set; }
}
