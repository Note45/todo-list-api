using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Moq;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Domain.Repositories;
using MockQueryable.Moq;

namespace TodoListAPI.Test.Application.Services
{
    public class TodoListServiceTest
    {
        [Fact(DisplayName = "Should be able to add a Todo to the TodoList")]
        public async void ShouldReturnTheTodoDataWhenAddUserTodoAsync()
        {
            var repositoryMock = new Mock<ITodoListRepository>();
            TodoListService todoListService = new(repositoryMock.Object);
            TodoEntity todoToAdd = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };

            repositoryMock.Setup(x => x.AddUserTodoAsync(todoToAdd)).ReturnsAsync(todoToAdd);

            var todoCreated = await todoListService.AddUserTodoAsync(todoToAdd);

            Assert.Equal(todoToAdd, todoCreated);
        }

        [Fact(DisplayName = "Should be able to delete a Todo to the TodoList")]
        public async void ShouldReturnTheTodoDataWhenRemoveUserTodoByIdAsync()
        {
            var repositoryMock = new Mock<ITodoListRepository>();
            TodoListService todoListService = new(repositoryMock.Object);
            TodoEntity todoToRemove = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };

            repositoryMock.Setup(x => x.RemoveUserTodoByDescriptionAsync(todoToRemove.UserId, todoToRemove.Id)).ReturnsAsync(true);

            var returnDeleteMethod = await todoListService.RemoveUserTodoByDescriptionAsync(todoToRemove.UserId, todoToRemove.Id);

            Assert.True(returnDeleteMethod);
        }

        [Fact(DisplayName = "Should be able to list all Todo from the user")]
        public async void ShouldListAllTodoDataWhenGetAllUserTodoAsync()
        {
            var repositoryMock = new Mock<ITodoListRepository>();
            TodoListService todoListService = new(repositoryMock.Object);
            var todoList = new List<TodoEntity>()
            {
                new TodoEntity
                {
                    Id = "test-id",
                    UserId = "test-userId",
                    Description = "Todo description test",
                    CreatedAt = new DateTime().ToString()
                }
            };

            repositoryMock.Setup(x => x.GetAllUserTodoAsync(todoList[0].UserId)).ReturnsAsync(todoList);

            var todoQuantity = await todoListService.GetAllUserTodoAsync(todoList[0].UserId);

            Assert.Single(todoQuantity);
        }

        [Fact(DisplayName = "Should be able to update a Todo")]
        public async void ShouldTodoDataWhenUpdateUserTodoAsync()
        {
            var repositoryMock = new Mock<ITodoListRepository>();
            TodoListService todoListService = new(repositoryMock.Object);
            TodoEntity todoUpdated = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description updated",
                CreatedAt = new DateTime().ToString()
            };

            repositoryMock.Setup(x => x.UpdateUserTodoAsync(todoUpdated)).ReturnsAsync(true);

            var returnUpdateMethod = await todoListService.UpdateUserTodoAsync(todoUpdated);

            Assert.True(returnUpdateMethod);
        }
    }
}