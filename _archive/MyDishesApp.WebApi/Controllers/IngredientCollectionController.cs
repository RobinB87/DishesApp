//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using MyDishesApp.Repository.Repositories.Interfaces;
//using System;

//namespace MyDishesApp.WebApi.Controllers
//{
//    /// <summary>
//    /// The ingredient collection controller
//    /// </summary>
//    [Produces("application/json")]
//    [Route("api/dishes/{dishId}/ingredientcollections")]
//    [ApiController]
//    [Authorize]
//    public class IngredientCollectionController : Controller
//    {
//        private readonly IMapper _mapper;
//        private readonly ILogger _logger;
//        private readonly IIngredientRepository _ingredientRepository;
//        private readonly IDishRepository _dishRepository;

//        /// <summary>
//        /// Initializes a new instance of <see cref="IngredientCollectionController" />
//        /// </summary>
//        /// <param name="logger">The logger to use</param>
//        /// <param name="mapper">The mapper to use</param>
//        /// <param name="ingredientRepository">The repository to use</param>
//        /// <param name="dishRepository">The repository to use</param>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientRepository" /> is null.</exception>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
//        public IngredientCollectionController(ILogger<IngredientCollectionController> logger, IMapper mapper, IIngredientRepository ingredientRepository, IDishRepository dishRepository)
//        {
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
//            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
//        }

//        //// Get
//        //[HttpGet("({ingredientIds})", Name = "GetIngredientCollection")]
//        //public async Task<ActionResult> GetIngredientCollection(int dishId,
//        //    [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ingredientIds)
//        //{
//        //    if (ingredientIds == null || !ingredientIds.Any())
//        //    {
//        //        return BadRequest();
//        //    }

//        //    if (!await _dishRepository.DishExists(dishId))
//        //    {
//        //        return NotFound();
//        //    }

//        //    var ingredientEntities = await _ingredientRepository.GetIngredientsForDishAsync(dishId, ingredientIds);

//        //    if (ingredientIds.Count() != ingredientEntities.Count())
//        //    {
//        //        return NotFound();
//        //    }

//        //    var ingredientCollectionToReturn = _mapper.Map<IEnumerable<IngredientDto>>(ingredientEntities);
//        //    return Ok(ingredientCollectionToReturn);
//        //}

//        //// Post IngredientCollection
//        //[HttpPost]
//        //public async Task<ActionResult> CreateIngredientCollection(
//        //   int dishId,
//        //   [FromBody] IEnumerable<IngredientForCreationDto> ingredientCollection)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    if (ingredientCollection == null)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    if (!await _dishRepository.DishExists(dishId))
//        //    {
//        //        return NotFound();
//        //    }

//        //    // Map IngredientCollection (Dto) to Entity
//        //    IEnumerable<Ingredient> newIngredientEntities = _mapper.Map<IEnumerable<Ingredient>>(ingredientCollection);

//        //    // Add ingredients to dish. If ingredientname already exists, quantities will be summed up.
//        //    try
//        //    {
//        //        await _ingredientRepository.AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantitiesAsync(newIngredientEntities, dishId);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw new Exception("Adding a collection of ingredients failed.", ex);
//        //    }

//        //    // return 201 created
//        //    var ingredientCollectionToReturn = _mapper.Map<IEnumerable<IngredientDto>>(newIngredientEntities);

//        //    var ingredientIdsAsString = string.Join(",", ingredientCollectionToReturn.Select(a => a.IngredientId));

//        //    return CreatedAtRoute("GetIngredientCollection",
//        //        new { dishId, ingredientIds = ingredientIdsAsString },
//        //        ingredientCollectionToReturn);
//        //}
//    }
//}
