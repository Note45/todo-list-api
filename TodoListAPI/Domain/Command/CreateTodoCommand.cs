using System.Text.Json.Serialization;
using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Command
{
    public class CreateTodoCommand
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime().ToString("u").Replace(" ", "T");

        public static explicit operator TodoEntity(CreateTodoCommand command)
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
