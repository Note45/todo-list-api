using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Moq;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Database.Config;

namespace TodoListAPI.Test.Infra.Repository
{
    public class TodoListServiceTest
    {
        [Fact(DisplayName = "Should be able to add a Todo to the TodoList")]
        public async void ShouldReturnTheTodoDataWhenAddUserTodoAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new(dbContextMock.Object);
            TodoListService todoListService = new(todoListRepository);
            TodoEntity todoToAdd = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };

            var todoCreated = await todoListService.AddUserTodoAsync(todoToAdd);

            Assert.Equal(todoToAdd, todoCreated);
        }

        [Fact(DisplayName = "Should be able to delete a Todo to the TodoList")]
        public async void ShouldReturnTheTodoDataWhenRemoveUserTodoByIdAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new(dbContextMock.Object);
            TodoListService todoListService = new(todoListRepository);
            TodoEntity todoToAdd = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };
            TodoEntity todoToAdd1 = new()
            {
                Id = "test-id-1",
                UserId = "test-userId",
                Description = "Todo description test 1",
                CreatedAt = new DateTime().ToString()
            };


            await todoListRepository.AddUserTodoAsync(todoToAdd);
            await todoListRepository.AddUserTodoAsync(todoToAdd1);

            var returnDeleteMethod = await todoListService.RemoveUserTodoByDescriptionAsync(todoToAdd1.UserId, todoToAdd1.Id);

            var todoQuantity = await todoListRepository.GetAllUserTodoAsync(todoToAdd1.UserId);

            Assert.Single(todoQuantity);
            Assert.True(returnDeleteMethod);
        }

        [Fact(DisplayName = "Should be able to list all Todo from the user")]
        public async void ShouldListAllTodoDataWhenGetAllUserTodoAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new(dbContextMock.Object);
            TodoListService todoListService = new(todoListRepository);
            TodoEntity todoToAdd = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };
            TodoEntity todoToAdd1 = new()
            {
                Id = "test-id-1",
                UserId = "test-userId",
                Description = "Todo description test 1",
                CreatedAt = new DateTime().ToString()
            };


            await todoListRepository.AddUserTodoAsync(todoToAdd);
            await todoListRepository.AddUserTodoAsync(todoToAdd1);

            var todoQuantity = await todoListService.GetAllUserTodoAsync(todoToAdd1.UserId);

            Assert.Equal(2, todoQuantity.Count);
        }

        [Fact(DisplayName = "Should be able to update a Todo")]
        public async void ShouldTodoDataWhenUpdateUserTodoAsync()
        {
            var dbContextMock = new Mock<DataContext>();
            TodoListRepository todoListRepository = new(dbContextMock.Object);
            TodoListService todoListService = new(todoListRepository);
            TodoEntity todoToAdd = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };
            TodoEntity todoUpdated = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description updated",
                CreatedAt = new DateTime().ToString()
            };


            await todoListRepository.AddUserTodoAsync(todoToAdd);

            var returnUpdateMethod = await todoListService.UpdateUserTodoAsync(todoUpdated);

            var todoSaved = await todoListRepository.GetAllUserTodoAsync(todoToAdd.UserId);

            Assert.Equal(todoUpdated.Id, todoSaved[0].Id);
            Assert.Equal(todoUpdated.UserId, todoSaved[0].UserId);
            Assert.Equal(todoUpdated.Description, todoSaved[0].Description);
            Assert.Equal(todoUpdated.CreatedAt, todoSaved[0].CreatedAt);
            Assert.True(returnUpdateMethod);
        }
    }
}