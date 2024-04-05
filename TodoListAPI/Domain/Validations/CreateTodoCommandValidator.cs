using FluentValidation;
using TodoListAPI.Domain.Command;

namespace TodoListAPI.Domain.Validations;

public class CreateTodoCommandValidator: AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(p => p.UserId).NotEmpty().WithMessage(MessagesValidator.UserIdInvalid);
        RuleFor(p => p.Description).NotEmpty().WithMessage(MessagesValidator.DescriptionInvalid);
    }
}