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
    public async Task<IActionResult> Post(Dish dish)
    {
        var addedDish = await _dishService.AddAsync(dish);
        return Ok(addedDish);
    }
}
