using System;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Domain.Mappers
{
    public static class UserMapper
    {
        public static UserData toData(UserEntity userEntity)
        {
            UserData userFormated = new()
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email,
                Password = userEntity.Password,
                CreatedAt = DateTime.Parse(userEntity.CreatedAt),
                UpdatedAt = DateTime.Parse(userEntity.UpdatedAt),
            };

            return userFormated;
        }
    }
}

