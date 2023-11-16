using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Test.Infra.Repository
{
    public class TodoListRepositoryTest
    {
        public Mock<IConfigurationSection> _mockConfSection;
        public Mock<IConfiguration> _mockConfiguration;
        public TodoListRepository _todoListRepository;

        public TodoListRepositoryTest()
        {
            _mockConfSection = new();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "WebApiDatabase")]).Returns("mock_value");

            _mockConfiguration = new();
            _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "WebApiDatabase"))).Returns(_mockConfSection.Object);
        }


        [Fact(DisplayName = "Should be able to add a Todo to the TodoList")]
        public async void ShouldReturnTheTodoDataWhenAddUserTodoAsync()
        {
            TodoEntity todoToAdd = new TodoEntity()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };

            var todoCreated = await _todoListRepository.AddUserTodoAsync(todoToAdd);

            Assert.Equal(todoToAdd, todoCreated);
        }

        [Fact(DisplayName = "Should be able to delete a by todo descriotion")]
        public async void ShouldReturnTheTodoDataWhenRemoveUserTodoByDescriptionAsync()
        {
            var data = new List<TodoData>
            {
                new TodoData            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime()
            },
            }.AsQueryable();

            TodoEntity todoToAdd = new TodoEntity()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };

            Mock<DbSet<TodoData>> mockTodosSet = new();
            mockTodosSet.As<IQueryable<TodoData>>().Setup(m => m.Provider).Returns(data.Provider);
            mockTodosSet.As<IQueryable<TodoData>>().Setup(m => m.Expression).Returns(data.Expression);
            mockTodosSet.As<IQueryable<TodoData>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockTodosSet.As<IQueryable<TodoData>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var todoContextMock = new Mock<DataContext>(_mockConfiguration.Object);
            todoContextMock.SetupGet(_ => _.TodoList).Returns(mockTodosSet.Object);

            var todoListRepository = new TodoListRepository(todoContextMock.Object);

            var returnDeleteMethod = await todoListRepository.RemoveUserTodoByDescriptionAsync(todoToAdd.UserId, todoToAdd.Description);

            Assert.True(returnDeleteMethod);
        }

        [Fact(DisplayName = "Should be able to delete a Todo by the Todo Id")]
        public async void ShouldReturnTheTodoDataWhenRemoveUserTodoByTodoIdAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new TodoListRepository(dbContextMock.Object);
            TodoEntity todoToAdd = new TodoEntity()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };
            TodoEntity todoToAdd1 = new TodoEntity()
            {
                Id = "test-id-1",
                UserId = "test-userId",
                Description = "Todo description test 1",
                CreatedAt = new DateTime().ToString()
            };


            await todoListRepository.AddUserTodoAsync(todoToAdd);
            await todoListRepository.AddUserTodoAsync(todoToAdd1);
            var returnDeleteMethod = await todoListRepository.RemoveUserTodoByTodoIdAsync(todoToAdd1.UserId, todoToAdd1.Id);
            var todoQuantity = await todoListRepository.GetAllUserTodoAsync(todoToAdd1.UserId);

            Assert.Single(todoQuantity);
            Assert.True(returnDeleteMethod);
        }

        [Fact(DisplayName = "Should be able to list all Todo from the user")]
        public async void ShouldListAllTodoDataWhenGetAllUserTodoAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new TodoListRepository(dbContextMock.Object);
            TodoEntity todoToAdd = new TodoEntity()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };
            TodoEntity todoToAdd1 = new TodoEntity()
            {
                Id = "test-id-1",
                UserId = "test-userId",
                Description = "Todo description test 1",
                CreatedAt = new DateTime().ToString()
            };


            await todoListRepository.AddUserTodoAsync(todoToAdd);
            await todoListRepository.AddUserTodoAsync(todoToAdd1);
            var todoQuantity = await todoListRepository.GetAllUserTodoAsync(todoToAdd1.UserId);

            Assert.Equal(2, todoQuantity.Count);
        }

        [Fact(DisplayName = "Should be able to update a Todo")]
        public async void ShouldTodoDataWhenUpdateUserTodoAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new TodoListRepository(dbContextMock.Object);
            TodoEntity todoToAdd = new TodoEntity()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };
            TodoEntity todoUpdated = new TodoEntity()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description updated",
                CreatedAt = new DateTime().ToString()
            };


            await todoListRepository.AddUserTodoAsync(todoToAdd);
            var returnUpdateMethod = await todoListRepository.UpdateUserTodoAsync(todoUpdated);
            var todoSaved = await todoListRepository.GetAllUserTodoAsync(todoToAdd.UserId);

            Assert.Equal(todoUpdated.Id, todoSaved[0].Id);
            Assert.Equal(todoUpdated.UserId, todoSaved[0].UserId);
            Assert.Equal(todoUpdated.Description, todoSaved[0].Description);
            Assert.Equal(todoUpdated.CreatedAt, todoSaved[0].CreatedAt);
            Assert.True(returnUpdateMethod);
        }
    }
}