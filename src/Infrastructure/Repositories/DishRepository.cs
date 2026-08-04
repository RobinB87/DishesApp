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

    public async Task<bool> Exists(string name) =>
        await _context.Dishes.AnyAsync(d => d.Name == name);

    public async Task<IEnumerable<Dish>> GetAllAsync() =>
        await _context.Dishes.ToListAsync();

    public async Task<Dish?> GetByGuidAsync(Guid guid) =>
        await _context.Dishes.FirstOrDefaultAsync(d => d.Guid == guid);

    public async Task<Dish?> GetByIdAsync(int id) =>
        await _context.Dishes
            .Include(d => d.DishIngredients)
            .ThenInclude(di => di.Ingredient)
            .FirstOrDefaultAsync(d => d.Id == id);

    public void Delete(Dish entity)
    {
        throw new NotImplementedException();
    }
}
