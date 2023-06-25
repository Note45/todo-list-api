using System;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Repositories;

namespace TodoListAPI.Infra.Repositories
{
	public class UserRepository: IUserRepository
    {
        private List<UserEntity> _usersList = new List<UserEntity>();

        public UserEntity AddUserAsync(UserEntity userData)
        {
            _usersList.Add(userData);

            return userData;
        }

        public UserEntity? GetUserById(string userId)
        {
            UserEntity? user = _usersList.Find(user => user.Id.Equals(userId));

            return user;
        }

        public bool RemoveUserByIdAsync(string userId)
        {
            var userToRemove = _usersList.Find(user => user.Id.Equals(userId));

            if (userToRemove is not null)
            {
                _usersList.Remove(userToRemove);
                return true;
            }

            return false;
        }

        public bool UpdateUserByIdAsync(UserEntity userData)
        {
            int userIndex = _usersList.FindIndex(user => user.Id.Equals(userData.Id));

            if (userIndex >= 0)
            {
                _usersList[userIndex] = userData;
                return true;
            }

            return false;
        }
    }
}

