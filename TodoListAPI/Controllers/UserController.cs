
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
    [Route("api/users")]
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
        public async Task<IActionResult> DeleteUser()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _userService.RemoveUserByIdAsync(userId);
            
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult?> UpdateUser([FromBody] UpdateUserCommand command, IValidator<UpdateUserCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(command);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _userService.AddUserAsync((UserEntity)command);
            
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest command, IValidator<LoginUserRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(command);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var token = await _authService.AuthUser(command);

            if (token is not null)
                return Ok(token);

            return Unauthorized();
        }
    }
}
