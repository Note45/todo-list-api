using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> AddUserAsync(UserEntity userData);
        public Task<UserEntity?> GetUserById(string userId);
        public Task<bool> RemoveUserByIdAsync(string userId);
        public Task<bool> UpdateUserByIdAsync(UserEntity userData);
    }
}
