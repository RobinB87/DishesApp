using Api.Contracts;
using Api.Contracts.V2;
using Api.Mapping;
using Application.Dishes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V2;

[Route("api/v2/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishService;
    private readonly IValidator<CreateDishRequest> _createDishRequestValidator;

    public DishesController(IDishService dishService,
        IValidator<CreateDishRequest> createDishRequestValidator)
    {
        _dishService = dishService;
        _createDishRequestValidator = createDishRequestValidator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult<DishResponseV2>>> Add(CreateDishRequest request)
    {
        var validationResult = await _createDishRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            return BadRequest(ApiResult<DishResponseV2>.Fail(new ProblemDetails
            {
                Title = "Validation failed",
                Detail = "One or more validation errors occurred.",
                Extensions = { ["errors"] = errors }
            }));
        }

        var addedDish = await _dishService.AddAsync(request);
        return Ok(ApiResult<DishResponseV2>.Ok(addedDish.ToResponseV2()));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<DishResponseBaseV2>>>> GetAll()
    {
        var dishes = await _dishService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<DishResponseBaseV2>>.Ok(dishes.Select(d => d.ToBaseResponseV2())));
    }

    [HttpGet("{guid}")]
    public async Task<ActionResult<ApiResult<DishResponseV2>>> GetByGuid(Guid guid)
    {
        var dish = await _dishService.GetByGuidAsync(guid);
        return dish is null
            ? NotFound(ApiResult<DishResponseV2>.Fail(new ProblemDetails { Title = $"Dish with guid {guid} not found" }))
            : Ok(ApiResult<DishResponseV2>.Ok(dish.ToResponseV2()));
    }
}
