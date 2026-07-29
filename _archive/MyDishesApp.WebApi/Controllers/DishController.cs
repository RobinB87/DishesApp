using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Services.Interfaces;
using MyDishesApp.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDishesApp.WebApi.Controllers
{
    /// <summary>
    /// The dish controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;

        /// <summary>
        /// Initializes a new instance of <see cref="DishController" />
        /// </summary>
        /// <param name="dishService">The dish service</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishService" /> is null.</exception>
        public DishController(IDishService dishService)
        {
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));
        }

        /// <summary>
        /// Get all dishes
        /// </summary>
        /// <returns>A list of dishes</returns>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllAsync()
        {
            return (await _dishService.GetAllAsync()).ToList();
        }

        /// <summary>
        /// Get a dish
        /// </summary>
        /// <returns>A dish</returns>
        [HttpGet("{id}", Name = "GetDish")]
        //[Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<DishDto>> GetById(int id)
        {
            var dish = await _dishService.GetByIdAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        /// <summary>
        /// Add a dish to the database
        /// </summary>
        /// <param name="dish">The dish</param>
        /// <returns>A 201 created when succesful</returns>
        [HttpPost]
        //[Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult> PostAsync(DishDto dish)
        {
            if (dish == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid || await _dishService.DishExistsAsync(dish.Name, ModelState))
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var dishToReturn = await _dishService.PostAsync(dish);
            return CreatedAtRoute("GetDish",
                new
                {
                    id = dishToReturn.DishId
                },
                dishToReturn);
        }

        /// <summary>
        /// Delete a dish from the database
        /// </summary>
        /// <param name="id">The dish id</param>
        /// <returns>204 when successful</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDish(int id)
        {
            await _dishService.DeleteAsync(id);
            return NoContent();
        }
    }
}




// TO BE REFACTORED:

// Old auth method:
//[HttpGet]
//public async Task<ActionResult> GetDishes()
//{
// TODO: Fix authentication
//    //if (_userInfoService.Role != "Administrator" || !Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
//    //{
//    //    return Forbid();
//    //}

//    // TODO: Add GetDishesForManager

//    var dishEntities = await _dishRepository.GetDishesAsync();
//    var dishes = _mapper.Map<IEnumerable<DishDto>>(dishEntities);
//    return Ok(dishes);
//}

//[HttpPatch("{dishId}")]
//public async Task<ActionResult> PartiallyUpdateDish(int dishId,
//    [FromBody] JsonPatchDocument<DishForUpdateDto> jsonPatchDocument)
//{
//    if (jsonPatchDocument == null)
//    {
//        return BadRequest();
//    }

//    var dishFromRepo = await _dishRepository.GetDishAsync(dishId);
//    if (dishFromRepo == null)
//    {
//        return BadRequest();
//    }

//    var dishToPatch = _mapper.Map<DishForUpdateDto>(dishFromRepo);

//    // if patchDocument is malformed, this is still a client error. Use ModelState
//    jsonPatchDocument.ApplyTo(dishToPatch, ModelState);
//    if (!ModelState.IsValid)
//    {
//        return new UnprocessableEntityObjectResult(ModelState);
//    }

//    if (!TryValidateModel(dishToPatch))
//    {
//        return new UnprocessableEntityObjectResult(ModelState);
//    }

//    _mapper.Map(dishToPatch, dishFromRepo);

//    if (!await _dishRepository.SaveAsync())
//    {
//        throw new Exception("Updating a dish failed on save.");
//    }

//    return NoContent();
//}