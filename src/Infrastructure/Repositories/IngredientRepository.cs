using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly AppDbContext _context;

    public IngredientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ingredient> AddAsync(Ingredient ingredient)
    {
        await _context.Ingredients.AddAsync(ingredient);
        await _context.SaveChangesAsync();
        return ingredient;
    }

    public async Task<IEnumerable<Ingredient>> GetAllAsync() =>
        await _context.Ingredients.ToListAsync();

    public async Task<Ingredient?> GetByIdAsync(int id) =>
        await _context.Ingredients.FindAsync(id);

    public async Task<Ingredient?> GetByNameAsync(string name) =>
        await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);

    public void Delete(Ingredient entity)
    {
        // TODO: verify if used by any dish
        throw new NotImplementedException();
    }
}