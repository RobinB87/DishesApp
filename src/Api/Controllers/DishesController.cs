using Api.Contracts;
using Api.Mapping;
using Application.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult<DishResponse>>> Add(CreateDishRequest request)
    {
        var addedDish = await _dishService.AddAsync(request);
        return Ok(ApiResult<DishResponse>.Ok(addedDish.ToResponse()));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<DishResponse>>>> GetAll()
    {
        var dishes = await _dishService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<DishResponse>>.Ok(dishes.Select(d => d.ToResponse())));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<DishResponse>>> GetById(int id)
    {
        var dish = await _dishService.GetByIdAsync(id);
        return dish is null
            ? NotFound(ApiResult<DishResponse>.Fail($"Dish with id {id} was not found."))
            : Ok(ApiResult<DishResponse>.Ok(dish.ToResponse()));
    }
}
