using Application.Repositories;
using Domain.Entities;

namespace Application.Dishes;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<Dish> AddAsync(Dish dish)
    {
        return await _dishRepository.AddAsync(dish);
    }
}