using Domain.Entities;

namespace Application.Repositories;

public interface IDishRepository : IRepository<Dish>
{
    Task<bool> Exists(string name);
}