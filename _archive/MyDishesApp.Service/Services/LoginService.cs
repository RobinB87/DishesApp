using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Dtos.Auth;
using MyDishesApp.Service.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly LoginSettings _loginSettings;

        /// <summary>
        /// Initializes a new instance of <see cref="LoginService" />
        /// </summary>
        /// <param name="mapper">The mapper</param>
        /// <param name="userRepository">The user repository</param>
        /// <param name="loginSettings">The login settings from config</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userRepository"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="loginSettings"/> is null</exception>
        public LoginService(IMapper mapper, IUserRepository userRepository, LoginSettings loginSettings)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _loginSettings = loginSettings ?? throw new ArgumentNullException(nameof(loginSettings));
        }

        /// <inheritdoc />
        public async Task<UserDto> GetUser(UserDto userDetails)
        {
            var user = await AuthenticateUser(userDetails);
            if (user == null)
            {
                return new UserDto();
            }

            user.Token = GenerateJwtToken(user);
            return user;
        }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns>A user</returns>
        private async Task<UserDto> AuthenticateUser(UserDto loginCredentials)
        {
            var userEntity = await _userRepository.GetUser(loginCredentials.Email, loginCredentials.Password);
            return _mapper.Map<UserDto>(userEntity);
        }

        /// <summary>
        /// Generate a Jwt token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>A token</returns>
        private string GenerateJwtToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_loginSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim("firstName", userInfo.FirstName.ToString()),
                new Claim("role",userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _loginSettings.Issuer,
                audience: _loginSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
