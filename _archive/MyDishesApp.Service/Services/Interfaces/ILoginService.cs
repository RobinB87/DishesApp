using MyDishesApp.Service.Dtos.Auth;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Services.Interfaces
{
    /// <summary>
    /// The login service
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Get the user details for login
        /// </summary>
        Task<UserDto> GetUser(UserDto userDetails);
    }
}
