using System;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Application.Requests
{
    public class LoginUserRequest
    {
        [FromBody] public string password { get; set; } = string.Empty;
        [FromRoute] public string email { get; set; } = string.Empty;

        public static implicit operator LoginUserCommand(LoginUserRequest request)
        {
            if (request is null) return default;

            return new LoginUserCommand
            {
                Password = request.password,
                Email = request.email
            };
        }
    }
}

