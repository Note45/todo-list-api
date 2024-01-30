using System;
using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Infra.Database.Models
{
    public class LoginUserCommand
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}

