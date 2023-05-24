using System;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Repositories;

namespace TodoListAPI.Infra
{
    public class TodoListRepository: ITodoListRepository
    {
		private List<TodoEntity> _todoList = new List<TodoEntity>();

        public TodoEntity AddUserTodoAsync(TodoEntity todoData)
        {
            _todoList.Add(todoData);

            return todoData;
        }

        public List<TodoEntity> GetAllUserTodoAsync(string userIdToCompare)
        {
            return _todoList.FindAll(todo => todo.UserId.Equals(userIdToCompare));
        }

        public Boolean RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoDescription)
        {
            var elementToRemove = _todoList.Find(todo => todo.Description.Equals(todoDescription) && todo.UserId.Equals(userIdToCompare));

            if (elementToRemove is not null) {
                return _todoList.Remove(elementToRemove);
            }

            return false;
        }

        public Boolean UpdateUserTodoAsync(TodoEntity todoData)
        {
            var indexToUpdate = _todoList.FindIndex(todo => todo.Id.Equals(todoData.Id));
                
            if (indexToUpdate >= 0)
            {
                _todoList[indexToUpdate] = todoData;
                return true;
            }


            return false;
        }
    }
}

