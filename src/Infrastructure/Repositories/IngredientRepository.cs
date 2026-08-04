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

    public async Task<IEnumerable<Ingredient>> AddManyAsync(IEnumerable<Ingredient> ingredients)
    {
        await _context.Ingredients.AddRangeAsync(ingredients);
        await _context.SaveChangesAsync();
        return ingredients;
    }

    public async Task<IEnumerable<Ingredient>> GetAllAsync() =>
        await _context.Ingredients.ToListAsync();

    public async Task<Ingredient?> GetByIdAsync(int id) =>
        await _context.Ingredients.FindAsync(id);

    public void Delete(Ingredient entity)
    {
        // TODO: verify if used by any dish
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Ingredient>> GetByNamesAsync(IEnumerable<string> names) =>
        await _context.Ingredients.Where(i => names.Contains(i.Name)).ToListAsync();
}