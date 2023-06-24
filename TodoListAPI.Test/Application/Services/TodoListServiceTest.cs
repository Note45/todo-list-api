using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra;

namespace TodoListAPI.Test.Infra.Repository
{
    public class TodoListServiceTest
    {
        [Fact(DisplayName = "Should be able to add a Todo to the TodoList")]
        public void ShouldReturnTheTodoDataWhenAddUserTodoAsync()
        {
            TodoListRepository todoListRepository = new();
            TodoListService todoListService = new(todoListRepository);
            TodoEntity todoToAdd = new()
            {
                Id = "test-id",
                UserId = "test-userId",
                Description = "Todo description test",
                CreatedAt = new DateTime().ToString()
            };

            var todoCreated = todoListService.AddUserTodoAsync(todoToAdd);

            Assert.Equal(todoToAdd, todoCreated);
        }

        [Fact(DisplayName = "Should be able to delete a Todo to the TodoList")]
        public void ShouldReturnTheTodoDataWhenRemoveUserTodoByIdAsync()
        {
            TodoListRepository todoListRepository = new();
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


            todoListRepository.AddUserTodoAsync(todoToAdd);
            todoListRepository.AddUserTodoAsync(todoToAdd1);

            var returnDeleteMethod = todoListService.RemoveUserTodoByDescriptionAsync(todoToAdd1.UserId, todoToAdd1.Description);

            var todoQuantity = todoListRepository.GetAllUserTodoAsync(todoToAdd1.UserId);

            Assert.Single(todoQuantity);
            Assert.True(returnDeleteMethod);
        }

        [Fact(DisplayName = "Should be able to list all Todo from the user")]
        public void ShouldListAllTodoDataWhenGetAllUserTodoAsync()
        {
            TodoListRepository todoListRepository = new();
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


            todoListRepository.AddUserTodoAsync(todoToAdd);
            todoListRepository.AddUserTodoAsync(todoToAdd1);

            var todoQuantity = todoListService.GetAllUserTodoAsync(todoToAdd1.UserId);

            Assert.Equal(2, todoQuantity.Count);
        }

        [Fact(DisplayName = "Should be able to update a Todo")]
        public void ShouldTodoDataWhenUpdateUserTodoAsync()
        {
            TodoListRepository todoListRepository = new();
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


            todoListRepository.AddUserTodoAsync(todoToAdd);

            var returnUpdateMethod = todoListService.UpdateUserTodoAsync(todoUpdated);

            var todoSaved = todoListRepository.GetAllUserTodoAsync(todoToAdd.UserId)[0];

            Assert.Equal(todoUpdated.Id, todoSaved.Id);
            Assert.Equal(todoUpdated.UserId, todoSaved.UserId);
            Assert.Equal(todoUpdated.Description, todoSaved.Description);
            Assert.Equal(todoUpdated.CreatedAt, todoSaved.CreatedAt);
            Assert.True(returnUpdateMethod);
        }
    }
}