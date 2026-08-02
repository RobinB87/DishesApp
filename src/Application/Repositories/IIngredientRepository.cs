using Domain.Entities;

namespace Application.Repositories;

public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<Ingredient?> GetByNameAsync(string name);
}