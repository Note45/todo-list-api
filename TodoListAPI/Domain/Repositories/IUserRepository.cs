using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity AddUserAsync(UserEntity userData);
        UserEntity? GetUserById(string userId);
        bool RemoveUserByIdAsync(string userId);
        bool UpdateUserByIdAsync(UserEntity userData);
    }
}
