namespace TodoListAPI.Domain.Validations;

public static class MessagesValidator
{
    public const string EmailInvalid = "Email field is invalid!";
    public const string PasswordInvalid = "Password field must be at least 8 characters long!";
    public const string NameInvalid = "Name field must not be empty!";
    public const string UserIdInvalid = "UserId field must not be empty!";
    public const string DescriptionInvalid = "Description field must not be empty!";
}