using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Services
{
    public interface ITodoListService
    {
        Task<List<TodoEntity>> GetAllUserTodoAsync(string UserIdToCompare);
        Task<TodoEntity> AddUserTodoAsync(TodoEntity todoData);
        Task<bool> RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoId);
        Task<bool> RemoveUserTodoByTodoIdAsync(string userIdToCompare, string todoId);
        Task<bool> UpdateUserTodoAsync(TodoEntity todoData);
    }
}
