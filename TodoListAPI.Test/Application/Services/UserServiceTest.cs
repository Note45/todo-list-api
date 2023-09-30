using Moq;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Repositories;

namespace TodoListAPI.Test.Infra.Repository
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "Should be able to add an user to the users list")]
        public async void ShouldReturnTheUserDataWhenAddUserAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            UserRepository userRepository = new UserRepository(dbContextMock);
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

            var userCreated = await userService.AddUserAsync(userData);

            Assert.Equal(userData, userCreated);
        }

        [Fact(DisplayName = "Should be able to get an user from the users list")]
        public async void ShouldReturnTheUserDataWhenGetUserAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            UserRepository userRepository = new UserRepository(dbContextMock);
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

            await userRepository.AddUserAsync(userData);

            var userSaved = await userService.GetUserById(userData.Id);

            Assert.Equal(userData, userSaved);
        }

        [Fact(DisplayName = "Should be able to remove an user from the users list")]
        public async void ShouldRemoveTheUserDataWhenRemoveUserByIdAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            UserRepository userRepository = new UserRepository(dbContextMock);
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

            await userRepository.AddUserAsync(userData);
            await userRepository.AddUserAsync(userData1);

            var isDeleted = await userService.RemoveUserByIdAsync(userData.Id);

            var userSaved = await userRepository.GetUserById(userData.Id);
            var userSaved1 = await userRepository.GetUserById(userData1.Id);

            Assert.True(isDeleted);
            Assert.Null(userSaved);
            Assert.Equal(userData1, userSaved1);
        }

        [Fact(DisplayName = "Should be able to update an user from the users list")]
        public async void ShouldUpdateTheUserDataWhenUpdateUserByIdAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            UserRepository userRepository = new UserRepository(dbContextMock);
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

            await userRepository.AddUserAsync(userData);

            var isUpdated = await userService.UpdateUserByIdAsync(userDataUpdated);

            var userSaved = await userRepository.GetUserById(userData.Id);

            Assert.True(isUpdated);
            Assert.Equal(userDataUpdated, userSaved);
        }
    }
}