using System;
using System.Net.Sockets;
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
        public Mock<IConfigurationSection> _mockConfSection;
        public Mock<IConfiguration> _mockConfiguration;

        public UserRepositoryTest()
        {
            _mockConfSection = new Mock<IConfigurationSection>();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "WebApiDatabase")]).Returns("mock_value");

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "WebApiDatabase"))).Returns(_mockConfSection.Object);
        }


        [Fact(DisplayName = "Should be able to add an user to the users list")]
        public async void ShouldReturnTheUserDataWhenAddUserAsync()
        {
            var userContextMock = new Mock<DataContext>(_mockConfiguration.Object);
            UserRepository userRepository = new UserRepository(userContextMock.Object);
            UserEntity userData = new UserEntity()
            {
                Id = "new-id-1",
                Name = "User Test Name",
                Email = "user@email.com",
                Password = "123password",
                CreatedAt = DateTime.Today.ToString(),
                UpdatedAt = DateTime.Today.ToString(),
            };

            var userCreated = await userRepository.AddUserAsync(userData);

            Assert.Equal(userData, userCreated);
        }

        [Fact(DisplayName = "Should be able to get an user from the users list")]
        public async void ShouldReturnTheUserDataWhenGetUserAsync()
        {
            var userContextMock = new Mock<DataContext>(_mockConfiguration.Object);
            Mock<DbSet<UserData>> mockUsersSet = new();
            userContextMock.SetupGet(_ => _.Users).Returns(mockUsersSet.Object);
            UserRepository userRepository = new UserRepository(userContextMock.Object);

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

            userContextMock.Setup(x => x.FindAsync(It.IsAny<System.Type>())).ReturnsAsync(userFormatedToData);

            var userSaved = await userRepository.GetUserById(userEntity.Id);

            Assert.Equal(userEntity, userSaved);
        }

        [Fact(DisplayName = "Should be able to remove an user from the users list")]
        public async void ShouldRemoveTheUserDataWhenRemoveUserByIdAsync()
        {
            var userContextMock = new Mock<DataContext>(_mockConfiguration.Object);
            UserRepository userRepository = new UserRepository(userContextMock.Object);
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

            var isDeleted = await userRepository.RemoveUserByIdAsync(userData.Id);

            var userSaved = await userRepository.GetUserById(userData.Id);
            var userSaved1 = await userRepository.GetUserById(userData1.Id);

            Assert.True(isDeleted);
            Assert.Null(userSaved);
            Assert.Equal(userData1, userSaved1);
        }

        [Fact(DisplayName = "Should be able to update an user from the users list")]
        public async void ShouldUpdateTheUserDataWhenUpdateUserByIdAsync()
        {
            var userContextMock = new Mock<DataContext>(_mockConfiguration.Object);
            UserRepository userRepository = new UserRepository(userContextMock.Object);
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

            var isUpdated = await userRepository.UpdateUserByIdAsync(userDataUpdated);

            var userSaved = await userRepository.GetUserById(userData.Id);

            Assert.True(isUpdated);
            Assert.Equal(userDataUpdated, userSaved);
        }
    }
}

