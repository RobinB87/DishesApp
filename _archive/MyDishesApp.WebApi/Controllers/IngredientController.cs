using Microsoft.AspNetCore.Mvc;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDishesApp.WebApi.Controllers
{
    /// <summary>
    /// The ingredient controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        /// <summary>
        /// Initializes a new instance of <see cref="IngredientController" />
        /// </summary>
        /// <param name="ingredientService">The repository to use</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientService" /> is null.</exception>
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
        }

        /// <summary>
        /// Get all ingredients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAllAsync()
        {
            return (await _ingredientService.GetAllAsync()).ToList();
        }

        //// Get
        //[HttpGet("{ingredientId}")]
        //public async Task<ActionResult> GetIngredientForDish(int dishId, int ingredientId)
        //{
        //    if (!await _dishRepository.DishExists(dishId))
        //    {
        //        return NotFound();
        //    }

        //    var ingredientFromRepo = await _ingredientRepository.GetIngredientForDishAsync(dishId, ingredientId);

        //    if (ingredientFromRepo == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(_mapper.Map<IngredientDto>(ingredientFromRepo));
        //}

        //[HttpGet]
        //public async Task<ActionResult> GetIngredientsForDish(int dishId)
        //{
        //    if (!await _dishRepository.DishExists(dishId))
        //    {
        //        return NotFound();
        //    }

        //    var ingredientsFromRepo = await _ingredientRepository.GetIngredientsForDishAsync(dishId);

        //    if (ingredientsFromRepo == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(_mapper.Map<IngredientDto[]>(ingredientsFromRepo));
        //}
       
        //// Patch
        //[HttpPatch("{ingredientId}")]
        //public async Task<ActionResult> PartiallyUpdateIngredient(int dishId, int ingredientId,
        //    [FromBody] JsonPatchDocument<IngredientForUpdateDto> jsonPatchDocument)
        //{
        //    if (jsonPatchDocument == null)
        //    {
        //        return BadRequest();
        //    }

        //    if (!await _dishRepository.DishExists(dishId))
        //    {
        //        return NotFound();
        //    }

        //    // Get ingredient entity from repo, to be patched.
        //    Ingredient ingredientFromRepo = await _ingredientRepository.GetIngredientForDishAsync(dishId, ingredientId);

        //    if (ingredientFromRepo == null)
        //    {
        //        return BadRequest();
        //    }

        //    IngredientForUpdateDto ingredientToPatch = _mapper.Map<IngredientForUpdateDto>(ingredientFromRepo);

        //    jsonPatchDocument.ApplyTo(ingredientToPatch, ModelState);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    if (!TryValidateModel(ingredientToPatch))
        //    {
        //        return BadRequest();
        //    }

        //    _mapper.Map(ingredientToPatch, ingredientFromRepo);

        //    if (!await _ingredientRepository.SaveAsync())
        //    {
        //        throw new Exception("Updating an ingredient failed on save.");
        //    }

        //    return NoContent();
        //}

        //// Delete
        //[HttpDelete("{ingredientId}")]
        //public async Task<ActionResult> DeleteIngredientFromDish(int dishId, int ingredientId)
        //{
        //    if (!await _dishRepository.DishExists(dishId))
        //    {
        //        return NotFound();
        //    }

        //    var ingredientFromRepository = await _ingredientRepository.GetIngredientForDishAsync(dishId, ingredientId);
        //    if (ingredientFromRepository == null)
        //    {
        //        return BadRequest();
        //    }

        //    _ingredientRepository.DeleteIngredientFromDish(ingredientFromRepository);

        //    if (!await _ingredientRepository.SaveAsync())
        //    {
        //        throw new Exception("Deleting an ingredient from dish failed on save.");
        //    }

        //    // add logger / mailservice?

        //    return NoContent();
        //}
    }
}