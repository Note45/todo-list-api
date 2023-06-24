using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Domain.Services;

namespace TodoListAPI.Application.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _todoListRepository; 

        public TodoListService(ITodoListRepository todoListRepository) { 
            _todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));
        }

        public TodoEntity AddUserTodoAsync(TodoEntity todoData)
        {
            return _todoListRepository.AddUserTodoAsync(todoData);
        }

        public List<TodoEntity> GetAllUserTodoAsync(string UserIdToCompare)
        {
            return _todoListRepository.GetAllUserTodoAsync(UserIdToCompare);
        }

        public bool RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoId)
        {
            return _todoListRepository.RemoveUserTodoByTodoIdAsync(userIdToCompare, todoId);
        }

        public bool UpdateUserTodoAsync(TodoEntity todoData)
        {
            return _todoListRepository.UpdateUserTodoAsync(todoData);
        }
    }
}
