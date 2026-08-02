using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is PostgresException postgresEx && postgresEx.SqlState == "23505")
                throw new InvalidOperationException($"An ingredient with the name '{ingredient.Name}' already exists.", ex);

            throw;
        }
        
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