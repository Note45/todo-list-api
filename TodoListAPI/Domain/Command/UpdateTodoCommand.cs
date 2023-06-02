using System.Text.Json.Serialization;
using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Command
{
    public class UpdateTodoCommand
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }

        public static explicit operator TodoEntity(UpdateTodoCommand command)
        {
            return new TodoEntity()
            {
                Id = command.Id,
                UserId = command.UserId,
                Description = command.Description,
                CreatedAt = command.CreatedAt,
            };
        }
    }
}
