using Domain.Entities;

namespace Application.Dishes;

public interface IDishService
{
    Task<Dish> AddAsync(Dish dish);
    Task<IEnumerable<Dish>> GetAllAsync();
}