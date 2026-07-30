using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        IList<Dish> dishes = new List<Dish>
        {
            new Dish("Spaghetti Bolognese", "Italy", "Cook pasta, prepare sauce, combine."),
            new Dish("Sushi", "Japan", "Prepare rice, slice fish, roll sushi."),
            new Dish("Tacos", "Mexico", "Cook meat, prepare toppings, assemble tacos."),
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(dishes);
        }

        [HttpPost]
        public IActionResult Post(Dish dish)
        {
            dishes.Add(dish);
            return Ok(dishes);
        }
    }
}
