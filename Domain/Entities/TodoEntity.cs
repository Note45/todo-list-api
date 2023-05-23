namespace TodoListAPI.Domain.Entities
{
    public class TodoEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
    }
}
