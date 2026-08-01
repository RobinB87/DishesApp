namespace Application.Repositories;

public interface IRepository<T>
{
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    void Delete(T entity);
}