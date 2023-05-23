using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Contracts
{
    public interface ITodoListRepository
    {
        List<TodoEntity> GetAllUserTodoAsync(string UserIdToCompare);
        TodoEntity AddUserTodoAsync(TodoEntity todoData);
        Boolean RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoDescription);
        Boolean UpdateUserTodoAsync(TodoEntity todoData);
    }
}
