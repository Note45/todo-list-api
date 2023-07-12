using System;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Domain.Mappers
{
    public static class TodoMapper
    {
        public static TodoEntity ToEntity(TodoData todoSaved)
        {
            TodoEntity todoFormated = new()
            {
                Id = todoSaved.Id,
                UserId = todoSaved.UserId,
                Description = todoSaved.Description,
                CreatedAt = todoSaved.CreatedAt.ToString(),
            };

            return todoFormated;
        }

    }
}

