using TodoListAPI.Domain.Entities;
using TodoListAPI.Infra.Database.Models;

namespace TodoListAPI.Domain.Mappers
{
    public static class UserMapper
    {
        public static UserData ToData(UserEntity userEntity)
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

        public static UserEntity ToEntity(UserData userSaved)
        {
            UserEntity userFormated = new()
            {
                Id = userSaved.Id,
                Name = userSaved.Name,
                Email = userSaved.Email,
                Password = userSaved.Password,
                CreatedAt = userSaved.CreatedAt.ToString(),
                UpdatedAt = userSaved.UpdatedAt.ToString(),
            };

            return userFormated;
        }
    }
}