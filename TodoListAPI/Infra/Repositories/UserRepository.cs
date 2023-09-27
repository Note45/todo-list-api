using System;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Mappers;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;

        public UserRepository(DataContext dbContext)
        {
            _db = dbContext ?? throw new Exception();
        }

        public async Task<UserEntity> AddUserAsync(UserEntity userData)
        {
            var userFormated = UserMapper.ToData(userData);

            await _db.AddAsync(userFormated);

            await _db.SaveChangesAsync();

            return userData;
        }

        public async Task<UserEntity?> GetUserById(string userId)
        {
            UserData? user = await _db.Users.FindAsync(userId);

            if (user is not null)
            {
                UserEntity userFormated = UserMapper.ToEntity(user);
                return userFormated;
            }

            return null;
        }

        public async Task<bool> RemoveUserByIdAsync(string userId)
        {
            var userToRemove = await _db.Users.FindAsync(userId);

            if (userToRemove is not null)
            {
                _db.Users.Remove(userToRemove);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUserByIdAsync(UserEntity userData)
        {
            var userSaved = await _db.Users.FindAsync(userData.Id);

            if (userSaved is not null)
            {
                UserData userSavedData = UserMapper.ToData(userData);

                _db.Users.Update(userSavedData);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}

