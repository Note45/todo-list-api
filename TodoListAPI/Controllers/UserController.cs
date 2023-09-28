using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Services;

namespace TodoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<UserEntity> PostUser([FromBody] CreateUserCommand command)
        {
            return await _userService.AddUserAsync((UserEntity)command);
        }
    }
}
