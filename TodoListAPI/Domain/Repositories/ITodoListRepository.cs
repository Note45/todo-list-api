using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Repositories
{
    public interface ITodoListRepository
    {
        List<TodoEntity> GetAllUserTodoAsync(string UserIdToCompare);
        TodoEntity AddUserTodoAsync(TodoEntity todoData);
        bool RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoDescription);
        bool RemoveUserTodoByTodoIdAsync(string userIdToCompare, string todoId);
        bool UpdateUserTodoAsync(TodoEntity todoData);
    }
}
