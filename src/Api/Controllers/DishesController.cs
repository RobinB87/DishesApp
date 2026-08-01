using Api.Contracts;
using Api.Mapping;
using Application.Dishes;
using Domain.Entities;
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
    public async Task<ActionResult<ApiResult<DishResponse>>> Add(Dish dish)
    {
        var addedDish = await _dishService.AddAsync(dish);
        return Ok(ApiResult<DishResponse>.Ok(addedDish.ToResponse()));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<DishResponse>>>> GetAll()
    {
        var dishes = await _dishService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<DishResponse>>.Ok(dishes.Select(d => d.ToResponse())));
    }
}
