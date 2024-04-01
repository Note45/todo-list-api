using FluentValidation;
using TodoListAPI.Domain.Command;

namespace TodoListAPI.Domain.Validations;

public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(p => p.Email).NotNull().EmailAddress().WithMessage(MessagesValidator.EmailInvalid);
        RuleFor(p => p.Password).NotEmpty().MinimumLength(8).WithMessage(MessagesValidator.PasswordInvalid);
        RuleFor(p => p.Name).NotEmpty().WithMessage(MessagesValidator.NameInvalid);
    }
}