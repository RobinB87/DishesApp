using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyDishesApp.Service.Dtos.Auth;
using MyDishesApp.Service.Services.Interfaces;
using MyDishesApp.WebApi.Authorization;
using System;
using System.Threading.Tasks;

namespace MyDishesApp.WebApi.Controllers
{
    /// <summary>
    /// The login controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        /// <summary>
        /// Initializes a new instance of <see cref="LoginController" />
        /// </summary>
        /// <param name="loginService">The login service</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="loginService" />is null.</exception>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }

        /// <summary>
        /// Method to be able to login
        /// </summary>
        /// <param name="userDetails">The user details</param>
        /// <returns>Login response</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserDto userDetails)
        {
            var user = await _loginService.GetUser(userDetails);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        /// <summary>
        /// Temp method... TODO: Remove
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        [Authorize(Policy = Policies.User)]
        public ActionResult Status()
        {
            return Ok();
        }
    }
}