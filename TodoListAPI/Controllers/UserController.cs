using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Services;

namespace TodoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
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
    }
}
