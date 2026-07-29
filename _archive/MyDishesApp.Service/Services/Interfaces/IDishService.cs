using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyDishesApp.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Services.Interfaces
{
    /// <summary>
    /// The dish service
    /// </summary>
    public interface IDishService
    {
        /// <summary>
        /// Get all dishes
        /// </summary>
        Task<IEnumerable<DishDto>> GetAllAsync();

        /// <summary>
        /// Get a dish by id
        /// </summary>
        Task<DishDto> GetByIdAsync(int id);

        /// <summary>
        /// Check if a dish exists
        /// </summary>
        Task<bool> DishExistsAsync(string name, ModelStateDictionary modelState);

        /// <summary>
        /// Add a dish
        /// </summary>
        Task<DishDto> PostAsync(DishDto dish);

        /// <summary>
        /// Delete a dish
        /// </summary>
        Task DeleteAsync(int id);
    }
}
