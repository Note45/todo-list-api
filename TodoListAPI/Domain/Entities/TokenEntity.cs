using System;
namespace TodoListAPI.Domain.Entities
{
    public class TokenEntity
    {
        public string? Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}

