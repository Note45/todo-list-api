using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Domain.Services;
using TodoListAPI.Infra.Auth;
using TodoListAPI.Domain.Helpers;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Interfaces;
using TodoListAPI.Domain.Command;

namespace TodoListAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHelper;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHelper = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public TokenEntity CreateUserToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }

        public async Task<TokenEntity?> AuthUser(LoginUserCommand command)
        {
            var user = await _userRepository.GetUserByEmail(command.Email);

            if (user is not null && _passwordHelper.Check(user.Password, command.Password).Verified)
            {

                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Email, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.Add(new(ClaimTypes.Role, UserRoles.User));


                return CreateUserToken(authClaims);
            }

            return null;
        }
    }
}

