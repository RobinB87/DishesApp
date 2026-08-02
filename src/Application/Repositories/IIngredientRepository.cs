using Domain.Entities;

namespace Application.Repositories;

public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<IEnumerable<Ingredient>> AddManyAsync(IEnumerable<Ingredient> ingredients);
    Task<IEnumerable<Ingredient>> GetByNamesAsync(IEnumerable<string> names);
}