using System;
namespace TodoListAPI.Infra.Database.Models
{
	public class UserData
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}

