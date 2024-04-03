using System.Text.Json.Serialization;

namespace TodoListAPI.Domain.Entities
{
    public class UserEntity
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
