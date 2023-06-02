using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Services
{
    public interface ITodoListService
    {
        List<TodoEntity> GetAllUserTodoAsync(string UserIdToCompare);
        TodoEntity AddUserTodoAsync(TodoEntity todoData);
        bool RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoId);
        bool UpdateUserTodoAsync(TodoEntity todoData);
    }
}
