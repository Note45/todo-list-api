using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Repositories;

namespace TodoListAPI.Test.Infra.Repository
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "Should be able to add an user to the users list")]
        public void ShouldReturnTheUserDataWhenAddUserAsync()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new(userRepository);
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            var userCreated = userService.AddUserAsync(userData);

            Assert.Equal(userData, userCreated);
        }

        [Fact(DisplayName = "Should be able to get an user from the users list")]
        public void ShouldReturnTheUserDataWhenGetUserAsync()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new(userRepository);
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            userRepository.AddUserAsync(userData);

            var userSaved = userService.GetUserById(userData.Id);

            Assert.Equal(userData, userSaved);
        }

        [Fact(DisplayName = "Should be able to remove an user from the users list")]
        public void ShouldRemoveTheUserDataWhenRemoveUserByIdAsync()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new(userRepository);
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };
            UserEntity userData1 = new UserEntity()
            {
                Id = "new-id-2",
                Name = "User Test Name 2",
                Email = "user2@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            userRepository.AddUserAsync(userData);
            userRepository.AddUserAsync(userData1);

            var isDeleted = userService.RemoveUserByIdAsync(userData.Id);

            var userSaved = userRepository.GetUserById(userData.Id);
            var userSaved1 = userRepository.GetUserById(userData1.Id);

            Assert.True(isDeleted);
            Assert.Null(userSaved);
            Assert.Equal(userData1, userSaved1);
        }

        [Fact(DisplayName = "Should be able to update an user from the users list")]
        public void ShouldUpdateTheUserDataWhenUpdateUserByIdAsync()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new(userRepository);
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };
            UserEntity userDataUpdated = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name 2",
                Email = "user2@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            userRepository.AddUserAsync(userData);

            var isUpdated = userService.UpdateUserByIdAsync(userDataUpdated);

            var userSaved = userRepository.GetUserById(userData.Id);

            Assert.True(isUpdated);
            Assert.Equal(userDataUpdated, userSaved);
        }
    }
}