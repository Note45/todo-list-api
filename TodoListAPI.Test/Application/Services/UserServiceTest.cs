using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Database.Models;
using TodoListAPI.Infra.Repositories;

namespace TodoListAPI.Test.Application.Services
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "Should be able to add an user to the users list")]
        public async void ShouldReturnTheUserDataWhenAddUserAsync()
        {
            var repositoryMock = new Mock<IUserRepository>();
            UserService userService = new(repositoryMock.Object);
            UserEntity user = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            repositoryMock.Setup(x => x.AddUserAsync(user)).ReturnsAsync(user);

            var userCreated = await userService.AddUserAsync(user);

            Assert.Equal(user, userCreated);
        }

        [Fact(DisplayName = "Should be able to get an user from the users list")]
        public async void ShouldReturnTheUserDataWhenGetUserAsync()
        {
            var repositoryMock = new Mock<IUserRepository>();
            UserService userService = new(repositoryMock.Object);
            UserEntity user = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            repositoryMock.Setup(x => x.GetUserById(user.Id)).ReturnsAsync(user);

            var userSaved = await userService.GetUserById(user.Id);

            Assert.Equal(user, userSaved);
        }

        [Fact(DisplayName = "Should be able to remove an user from the users list")]
        public async void ShouldRemoveTheUserDataWhenRemoveUserByIdAsync()
        {
            var repositoryMock = new Mock<IUserRepository>();
            UserService userService = new(repositoryMock.Object);
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            repositoryMock.Setup(x => x.RemoveUserByIdAsync(userData.Id)).ReturnsAsync(true);

            var isDeleted = await userService.RemoveUserByIdAsync(userData.Id);

            Assert.True(isDeleted);
        }

        [Fact(DisplayName = "Should be able to update an user from the users list")]
        public async void ShouldUpdateTheUserDataWhenUpdateUserByIdAsync()
        {
            var repositoryMock = new Mock<IUserRepository>();
            UserService userService = new(repositoryMock.Object);
            UserEntity userDataUpdated = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            repositoryMock.Setup(x => x.UpdateUserByIdAsync(userDataUpdated)).ReturnsAsync(true);

            var isUpdated = await userService.UpdateUserByIdAsync(userDataUpdated);

            Assert.True(isUpdated);
        }
    }
}