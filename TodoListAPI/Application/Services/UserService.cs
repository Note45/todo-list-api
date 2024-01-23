using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Domain.Services;

namespace TodoListAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Task<UserEntity> AddUserAsync(UserEntity userData)
        {
            return _userRepository.AddUserAsync(userData);
        }


        public Task<UserEntity?> GetUserById(string userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public Task<UserEntity?> GetUserByEmail(string userEmail)
        {
            return _userRepository.GetUserByEmail(userEmail);
        }

        public Task<bool> RemoveUserByIdAsync(string userId)
        {
            return _userRepository.RemoveUserByIdAsync(userId);
        }

        public Task<bool> UpdateUserByIdAsync(UserEntity userData)
        {
            return _userRepository.UpdateUserByIdAsync(userData);
        }
    }
}
