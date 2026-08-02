using Api.Contracts;
using Domain.Entities;

namespace Application.Dishes;

public interface IDishService
{
    Task<Dish> AddAsync(CreateDishRequest request);
    Task<IEnumerable<Dish>> GetAllAsync();
    Task<Dish?> GetByIdAsync(int id);
}
