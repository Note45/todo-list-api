using System;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Mappers;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Database.Models;
using TodoListAPI.Infra.Repositories;

namespace TodoListAPI.Test.Infra.Repositories
{
    public class UserRepositoryTest
    {
        public Mock<DataContext> _userContextMock;
        public UserRepository _userRepository;

        public UserRepositoryTest()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "WebApiDatabase")]).Returns("mock_value");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "WebApiDatabase"))).Returns(mockConfSection.Object);

            _userContextMock = new Mock<DataContext>(mockConfiguration.Object);
            Mock<DbSet<UserData>> mockUsersSet = new();
            _userContextMock.SetupGet(_ => _.Users).Returns(mockUsersSet.Object);
            _userRepository = new UserRepository(_userContextMock.Object);
        }


        [Fact(DisplayName = "Should be able to add an user to the users list")]
        public async void ShouldReturnTheUserDataWhenAddUserAsync()
        {
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            var userCreated = await _userRepository.AddUserAsync(userData);

            Assert.Equal(userData, userCreated);
        }

        [Fact(DisplayName = "Should be able to get an user from the users list")]
        public async void ShouldReturnTheUserDataWhenGetUserAsync()
        {
            UserEntity userEntity = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };
            UserData? userFormatedToData = UserMapper.ToData(userEntity);

            _userContextMock.Setup(x => x.Users.FindAsync(userFormatedToData.Id)).ReturnsAsync(userFormatedToData);

            var userSaved = await _userRepository.GetUserById(userEntity.Id);

            Assert.Equivalent(userEntity, userSaved);
        }

        [Fact(DisplayName = "Should be able to remove an user from the users list")]
        public async void ShouldRemoveTheUserDataWhenRemoveUserByIdAsync()
        {
            UserEntity userEntity = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };
            UserData? userFormatedToData = UserMapper.ToData(userEntity);

            _userContextMock.Setup(x => x.Users.FindAsync(userFormatedToData.Id)).ReturnsAsync(userFormatedToData);

            var isDeleted = await _userRepository.RemoveUserByIdAsync(userEntity.Id);

            Assert.True(isDeleted);
        }

        [Fact(DisplayName = "Should be able to update an user from the users list")]
        public async void ShouldUpdateTheUserDataWhenUpdateUserByIdAsync()
        {
            UserEntity userEntity = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };
            UserData? userFormatedToData = UserMapper.ToData(userEntity);

            UserEntity userDataUpdated = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name 2",
                Email = "user2@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            _userContextMock.Setup(x => x.Users.FindAsync(userFormatedToData.Id)).ReturnsAsync(userFormatedToData);

            var isUpdated = await _userRepository.UpdateUserByIdAsync(userDataUpdated);

            Assert.True(isUpdated);
        }
    }
}

