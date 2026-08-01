using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;

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
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Ingredient>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Ingredient> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Ingredient entity)
    {
        throw new NotImplementedException();
    }
}