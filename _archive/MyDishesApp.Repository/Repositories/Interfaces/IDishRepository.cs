using System.Collections.Generic;
using System.Threading.Tasks;
using MyDishesApp.Repository.Data.Entities;

namespace MyDishesApp.Repository.Repositories.Interfaces
{
    /// <summary>
    /// The dish repository
    /// </summary>
    public interface IDishRepository
    {
        /// <summary>
        /// Check if a dish exists by name
        /// </summary>
        /// <param name="name">The name</param>
        Task<bool> DishExistsAsync(string name);

        /// <summary>
        /// Get all dishes
        /// </summary>
        Task<IEnumerable<Dish>> GetDishesAsync();

        /// <summary>
        /// Get a dish by id
        /// </summary>
        /// <param name="id">The dish id</param>
        Task<Dish> GetDishAsync(int id);

        /// <summary>
        /// Add a dish to the database
        /// </summary>
        /// <param name="dish">The dish</param>
        Task AddDishAsync(Dish dish);

        /// <summary>
        /// Delete a dish from the database
        /// </summary>
        /// <param name="dish">The dish</param>
        void DeleteDish(Dish dish);

        /// <summary>
        /// Method for saving the context
        /// </summary>
        Task<bool> SaveAsync();
    }
}
