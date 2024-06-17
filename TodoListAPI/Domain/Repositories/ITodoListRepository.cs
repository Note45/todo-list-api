using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Repositories
{
    public interface ITodoListRepository
    {
        public Task<List<TodoEntity>> GetAllUserTodoAsync(string UserIdToCompare);
        public Task<TodoEntity> AddUserTodoAsync(TodoEntity todoData);
        public Task<bool> RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoDescription);
    public Task<bool> RemoveUserTodoByTodoIdAsync(string userIdToCompare, string todoId);
        public Task<bool> UpdateUserTodoAsync(TodoEntity todoData);
    }
}
