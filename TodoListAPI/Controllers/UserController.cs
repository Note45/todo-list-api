
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Services;
using TodoListAPI.Application.Requests;
using TodoListAPI.Infra.Auth;
using Microsoft.AspNetCore.Authorization;

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
        [Route("")]
        public async Task<IActionResult?> PostUser([FromBody] CreateUserCommand command, IValidator<CreateUserCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(command);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var user = await _userService.AddUserAsync((UserEntity)command);
            
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        [Route("")]
        public async Task<IActionResult?> GetUser()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserById(userId);
            
            if (user is null)
                return NotFound();
            
            return Ok(user);
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.User)]
        [Route("")]
        public async Task<bool> DeleteUser()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            return await _userService.RemoveUserByIdAsync(userId);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        [Route("")]
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
