using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Services;
using TodoListAPI.Application.Requests;
using TodoListAPI.Domain.Helpers;
using System.IdentityModel.Tokens.Jwt;
using TodoListAPI.Infra.Auth;

namespace TodoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<UserEntity> PostUser([FromBody] CreateUserCommand command)
        {
            return await _userService.AddUserAsync((UserEntity)command);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<UserEntity?> GetUser([FromRoute(Name = "userId")] string userId)
        {
            return await _userService.GetUserById(userId);
        }

        [HttpDelete]
        [Route("{userId}/delete")]
        public async Task<bool> DeleteUser([FromRoute(Name = "userId")] string userId)
        {
            return await _userService.RemoveUserByIdAsync(userId);
        }

        [HttpPut]
        [Route("update")]
        public async Task<UserEntity> UpdateUser([FromBody] UpdateUserCommand command)
        {
            return await _userService.AddUserAsync((UserEntity)command);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest command)
        {
            var token = await _authService.AuthUser(command);

            if (token is not null)
                return Ok(token);

            return Unauthorized();
        }
    }
}
