using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Services
{
    public interface IUserService
    {
        UserEntity AddUserAsync(UserEntity userData);
        UserEntity? GetUserById(string userId);
        bool RemoveUserByIdAsync(string userId);
        bool UpdateUserByIdAsync(UserEntity userData);
    }
}
