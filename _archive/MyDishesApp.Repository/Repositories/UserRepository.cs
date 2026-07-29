using Microsoft.EntityFrameworkCore;
using MyDishesApp.Repository.Data;
using MyDishesApp.Repository.Data.Entities.Auth;
using MyDishesApp.Repository.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MyDishesApp.Repository.Repositories
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        private readonly DishesContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="UserRepository" />
        /// </summary>
        /// <param name="context">The dishes context</param>
        public UserRepository(DishesContext context)
        {
            _context = context;
        }

        // <inheritdoc />
        public async Task<User> GetUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Email.ToLower() == email.ToLower() && u.Password == password);
        }
    }
}
