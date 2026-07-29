using Microsoft.EntityFrameworkCore;
using MyDishesApp.Repository.Data;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Repository.Repositories
{
    /// <inheritdoc />
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DishesContext _context;
        private readonly IDishRepository _dishRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="IngredientRepository" />
        /// </summary>
        /// <param name="context">The dishes context</param>
        /// <param name="dishRepository">The dish repository context</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository"/> is null</exception>
        public IngredientRepository(DishesContext context, IDishRepository dishRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        }

        // <inheritdoc />
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // <inheritdoc />
        public async Task<Ingredient> GetIngredientAsync(string name)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
        }

        //// <inheritdoc />
        //public async Task AddIngredient(Ingredient ingredient)
        //{
        //    await _context.Ingredients.AddAsync(ingredient);
        //}

        //public async Task<IEnumerable<Ingredient>> GetIngredientsForDishAsync(int dishId)
        //{
        //    return await _context.Ingredients.Where(i => i.DishId == dishId).ToListAsync();
        //}

        //public async Task<IEnumerable<Ingredient>> GetIngredientsForDishAsync(int dishId, IEnumerable<int> ingredientIds)
        //{
        //    return await _context.Ingredients
        //         .Where(i => i.DishId == dishId && ingredientIds.Contains(i.IngredientId)).ToListAsync();
        //}

        //public async Task<Ingredient> GetIngredientForDishAsync(int dishId, int ingredientId)
        //{
        //    return await _context.Ingredients
        //        .Where(i => i.DishId == dishId && i.IngredientId == ingredientId).FirstOrDefaultAsync();
        //}

        //public async Task AddIngredientToDishAsync(int dishId, Ingredient ingredient)
        //{
        //    var dish = await _dishRepository.GetDishAsync(dishId);
        //    if (dish == null)
        //    {
        //        // throw an exception - this is a race condition
        //        // that's mostly caught by checking if the tour exists
        //        // right before calling into this method - if that method is not
        //        // called the condition can happen, otherwise the tour
        //        // will already be loaded on the context
        //        throw new Exception($"Cannot add ingredient to dish with id {dishId}: dish not found.");
        //    }
        //    dish.Ingredients.Add(ingredient);
        //}

        //public void DeleteIngredientFromDish(Ingredient ingredient)
        //{
        //    _context.Ingredients.Remove(ingredient);
        //}

        //public async Task AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantitiesAsync(IEnumerable<Ingredient> newIngredientEntities, int dishId)
        //{
        //    // Get ingredients for dish and transform IEnumerable to List (for modification of list)
        //    IEnumerable<Ingredient> existingIngredients = await GetIngredientsForDishAsync(dishId);
        //    List<Ingredient> existingIngredientsToBeModified = existingIngredients.ToList();

        //    // Loop over new Ingredients and compare with existing ingredients.
        //    foreach (Ingredient newIngredient in newIngredientEntities)
        //    {
        //        var ingredientExists = false;
        //        foreach (Ingredient existingIngredient in existingIngredientsToBeModified)
        //        {
        //            if (newIngredient.Name != existingIngredient.Name)
        //            {
        //                continue;
        //            }
        //            // If new and existing name match, sum up quantitiy and save.
        //            existingIngredient.Quantity += newIngredient.Quantity;
        //            ingredientExists = true;
        //            if (!await SaveAsync())
        //            {
        //                throw new Exception("Adding a collection of ingredients failed on save.");
        //            }
        //        }

        //        if (!ingredientExists)
        //        {
        //            existingIngredientsToBeModified.Add(newIngredient);
        //            await AddIngredientToDishAsync(dishId, newIngredient);
        //            if (!await SaveAsync())
        //            {
        //                throw new Exception("Adding a collection of ingredients failed on save.");
        //            }
        //        }
        //    }
        //}

        /// <inheritdoc />
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
