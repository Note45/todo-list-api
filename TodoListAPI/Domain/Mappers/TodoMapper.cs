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

        public static TodoData ToData(TodoEntity todoEntity)
        {
            TodoData todoFormated = new()
            {
                Id = todoEntity.Id,
                UserId = todoEntity.UserId,
                Description = todoEntity.Description,
                CreatedAt = DateTime.Parse(todoEntity.CreatedAt),
            };

            return todoFormated;
        }
    }
}

