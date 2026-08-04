using FluentValidation;

namespace Api.Contracts;

public class CreateDishIngredientRequest
{
    public required string IngredientName { get; set; }
    public double PricePerUnit { get; set; }
    public double Quantity { get; set; }
}

public class CreateDishIngredientRequestValidator : AbstractValidator<CreateDishIngredientRequest>
{
    public CreateDishIngredientRequestValidator()
    {
        RuleFor(x => x.IngredientName)
            .NotEmpty().WithMessage("Ingredient name is required.")
            .MaximumLength(100).WithMessage("Ingredient name must not exceed 100 characters.");
        
        RuleFor(x => x.PricePerUnit)
            .GreaterThanOrEqualTo(0).WithMessage("Price per unit must be a positive value.");
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be a positive value.");
    }
}
