using System.Threading.Tasks;
using MyDishesApp.Repository.Data.Entities.Auth;

namespace MyDishesApp.Repository.Repositories.Interfaces
{
    /// <summary>
    /// The user repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get a user from the database
        /// </summary>
        /// <param name="email">The email for login</param>
        /// <param name="password">The password for login</param>
        /// <returns></returns>
        Task<User> GetUser(string email, string password);
    }
}