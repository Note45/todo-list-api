using System.Security.Claims;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Services
{
    public interface IAuthService
    {
        public TokenEntity CreateUserToken(List<Claim> authClaims);
        public Task<TokenEntity?> AuthUser(LoginUserCommand command);
    }
}

