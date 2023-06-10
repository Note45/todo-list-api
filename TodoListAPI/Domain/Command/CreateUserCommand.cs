using System.Text.Json.Serialization;
using TodoListAPI.Domain.Entities;

namespace TodoListAPI.Domain.Command
{
    public class CreateUserCommand
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime().ToString("u").Replace(" ", "T");
        public string UpdatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime().ToString("u").Replace(" ", "T");

        public static explicit operator UserEntity(CreateUserCommand command)
        {
            return new UserEntity()
            {
                Id = command.Id,
                Name = command.Name,
                Password = command.Password,
                CreatedAt = command.CreatedAt,
                UpdatedAt = command.UpdatedAt,
            };
        }
    }
}
