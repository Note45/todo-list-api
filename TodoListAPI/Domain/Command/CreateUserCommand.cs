using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Helpers;
using TodoListAPI.Infra.Configs;

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
            PasswordHasher passwordHasher = new();

            return new UserEntity()
            {
                Id = command.Id,
                Name = command.Name,
                Email = command.Email,
                Password = passwordHasher.Hash(command.Password),
                CreatedAt = command.CreatedAt,
                UpdatedAt = command.UpdatedAt,
            };
        }
    }
}
