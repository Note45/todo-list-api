namespace TodoListAPI.Domain.Command
{
    public class LoginUserCommand
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}

