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
    public class DishRepository : IDishRepository
    {
        private readonly DishesContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="DishRepository" />
        /// </summary>
        /// <param name="context">The dishes context</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is null</exception>
        public DishRepository(DishesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // <inheritdoc />
        public async Task<bool> DishExistsAsync(string name)
        {
            return await _context.Dishes.AnyAsync(d =>
                d.Name.ToLower() == name.ToLower()); 
        }

        // <inheritdoc />
        public async Task<IEnumerable<Dish>> GetDishesAsync()
        {
            return await _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(d => d.Ingredient)
                .ToListAsync();
        }

        // <inheritdoc />
        public async Task<Dish> GetDishAsync(int id)
        {
            return await _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(d => d.Ingredient)
                .FirstOrDefaultAsync(d => d.DishId == id);
        }

        // <inheritdoc />
        public async Task AddDishAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
        }

        // <inheritdoc />
        public void DeleteDish(Dish dish)
        {
            _context.Dishes.Remove(dish);
        }

        /// <inheritdoc />
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
