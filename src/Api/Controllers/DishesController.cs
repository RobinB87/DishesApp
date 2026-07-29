using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDishes()
        {
            // Logic to retrieve dishes from the database or service
            var dishes = new List<string> { "Spaghetti", "Pizza", "Salad" };
            return Ok(dishes);
        }
    }
}
