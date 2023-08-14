using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Mappers;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Infra.Database.Config;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Infra
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly DataContext _db;

        public TodoListRepository(DataContext dbContext)
        {
            _db = dbContext ?? throw new Exception();
        }

        public async Task<TodoEntity> AddUserTodoAsync(TodoEntity todoData)
        {
            await _db.AddAsync(todoData);

            return todoData;
        }

        public async Task<List<TodoEntity>> GetAllUserTodoAsync(string userIdToCompare)
        {
            List<TodoData> todoListSaved = await _db.TodoList.Where(todo => todo.UserId == userIdToCompare).ToListAsync();

            List<TodoEntity> fomatedTodoList = new List<TodoEntity>();
            foreach (TodoData todo in todoListSaved)
            {
                var todoFormated = TodoMapper.ToEntity(todo);
                fomatedTodoList.Add(todoFormated);
            }

            return fomatedTodoList;
        }

        public async Task<bool> RemoveUserTodoByDescriptionAsync(string userIdToCompare, string todoDescription)
        {
            var elementToRemove = _todoList.Find(todo => todo.Description.Equals(todoDescription) && todo.UserId.Equals(userIdToCompare));

            if (elementToRemove is not null)
            {
                return _todoList.Remove(elementToRemove);
            }

            return false;
        }

        public async Task<bool> RemoveUserTodoByTodoIdAsync(string userIdToCompare, string todoId)
        {
            var elementToRemove = _todoList.Find(todo => todo.Id.Equals(todoId) && todo.UserId.Equals(userIdToCompare));

            if (elementToRemove is not null)
            {
                return _todoList.Remove(elementToRemove);
            }

            return false;
        }

        public async Task<bool> UpdateUserTodoAsync(TodoEntity todoData)
        {
            var indexToUpdate = _todoList.FindIndex(todo => todo.Id.Equals(todoData.Id));

            if (indexToUpdate >= 0)
            {
                _todoList[indexToUpdate].Description = todoData.Description;
                return true;
            }


            return false;
        }
    }
}

