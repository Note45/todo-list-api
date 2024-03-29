﻿using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Services
{
    public interface IUserService
    {
        public Task<UserEntity> AddUserAsync(UserEntity userData);
        public Task<UserEntity?> GetUserById(string userId);
        public Task<UserEntity?> GetUserByEmail(string userEmail);
        public Task<bool> RemoveUserByIdAsync(string userId);
        public Task<bool> UpdateUserByIdAsync(UserEntity userData);
    }
}
