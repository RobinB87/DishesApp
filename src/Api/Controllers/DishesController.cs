using Api.Contracts;
using Api.Mapping;
using Application.Dishes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
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
    public async Task<ActionResult<ApiResult<DishResponse>>> Add(CreateDishRequest request)
    {
        var validationResult = await _createDishRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            return BadRequest(ApiResult<DishResponse>.Fail(new ProblemDetails
            {
                Title = "Validation failed",
                Detail = "One or more validation errors occurred.",
                Extensions = { ["errors"] = errors }
            }));
        }

        var addedDish = await _dishService.AddAsync(request);
        return Ok(ApiResult<DishResponse>.Ok(addedDish.ToResponse()));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<DishResponseBase>>>> GetAll()
    {
        var dishes = await _dishService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<DishResponseBase>>.Ok(dishes.Select(d => d.ToBaseResponse())));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<DishResponse>>> GetById(int id)
    {
        var dish = await _dishService.GetByIdAsync(id);
        return dish is null
            ? NotFound(ApiResult<DishResponse>.Fail(new ProblemDetails { Title = $"Dish with id {id} not found" }))
            : Ok(ApiResult<DishResponse>.Ok(dish.ToResponse()));
    }
}
