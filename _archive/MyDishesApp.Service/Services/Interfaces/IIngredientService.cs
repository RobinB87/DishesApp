using MyDishesApp.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Services.Interfaces
{
    /// <summary>
    /// The ingredient service
    /// </summary>
    public interface IIngredientService
    {
        /// <summary>
        /// Get all ingredients
        /// </summary>
        Task<IEnumerable<IngredientDto>> GetAllAsync();
    }
}
