using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DishRepository : IDishRepository
{
    private readonly AppDbContext _context;

    public DishRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Dish> AddAsync(Dish dish)
    {
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();
        return dish;
    }

    public async Task<IEnumerable<Dish>> GetAllAsync() =>
        await _context.Dishes.ToListAsync();

    public Task<Dish> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Dish entity)
    {
        throw new NotImplementedException();
    }
}
