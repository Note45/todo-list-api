using FluentValidation;
using TodoListAPI.Application.Requests;

namespace TodoListAPI.Domain.Validations;

public class LoginUserRequestValidator: AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(p => p.email).NotNull().EmailAddress().WithMessage(MessagesValidator.EmailInvalid);
        RuleFor(p => p.password).NotEmpty().MinimumLength(8).WithMessage(MessagesValidator.PasswordInvalid);
    }
}