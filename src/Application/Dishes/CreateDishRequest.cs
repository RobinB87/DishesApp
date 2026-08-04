using Application.Repositories;
using FluentValidation;

namespace Api.Contracts;

public class CreateDishRequest
{
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Recipe { get; set; }
    public List<CreateDishIngredientRequest> Ingredients { get; set; } = [];
}

public class CreateDishRequestValidator : AbstractValidator<CreateDishRequest>
{
    private readonly IDishRepository _dishRepository;

    public CreateDishRequestValidator(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dish name is required.")
            .MaximumLength(100).WithMessage("Dish name must not exceed 100 characters.")
            .MustAsync(DishNameUnique).WithMessage("A dish with the same name already exists.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Dish country is required.")
            .MaximumLength(100).WithMessage("Dish country must not exceed 100 characters.");

        RuleFor(x => x.Recipe)
            .NotEmpty().WithMessage("Dish recipe is required.")
            .MaximumLength(500).WithMessage("Dish recipe must not exceed 500 characters.");

        RuleForEach(x => x.Ingredients).SetValidator(new CreateDishIngredientRequestValidator());
    }

    private async Task<bool> DishNameUnique(string name, CancellationToken ct) => 
        !await _dishRepository.Exists(name);
}
