using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Moq;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Interfaces;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Infra.Auth;

namespace TodoListAPI.Test.Application.Services
{
    public class AuthServiceTest
    {
        IConfiguration _configurationMock;

        public AuthServiceTest()
        {

            var inMemorySettings = new Dictionary<string, string> {
                {"JWT:Secret", "mock_value_key_test_key_test_key"},
                {"JWT:ValidIssuer", "mock_value1"},
                {"JWT:ValidAudience", "mock_value2"},
            };

            _configurationMock = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        }

        [Fact(DisplayName = "Should be able to create a login token")]
        public void ShouldReturnTokenLoginTest()
        {

            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();
            var authService = new AuthService(_configurationMock, userRepositoryMock.Object, passwordHasherMock.Object);

            var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Email, "teste@teste.com"),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role, UserRoles.User)
                };

            var result = authService.CreateUserToken(authClaims);

            Assert.IsType<TokenEntity>(result);
            Assert.IsType<string>(result.Token);
            Assert.IsType<DateTime>(result.ValidTo);
        }

        [Fact(DisplayName = "Should be able to authentica the user")]
        public async void ShouldAuthenticaTheUserLoginTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();
            var authService = new AuthService(_configurationMock, userRepositoryMock.Object, passwordHasherMock.Object);

            UserEntity user = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "hashed_password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            var command = new LoginUserCommand
            {
                Password = "123456",
                Email = user.Email
            };

            userRepositoryMock.Setup(x => x.GetUserByEmail(command.Email)).ReturnsAsync(user);

            passwordHasherMock.Setup(x => x.Check(user.Password, command.Password)).Returns((true, false));

            var result = await authService.AuthUser(command);

            Assert.IsType<TokenEntity>(result);
            Assert.IsType<string>(result.Token);
            Assert.IsType<DateTime>(result.ValidTo);
        }
    }
}

