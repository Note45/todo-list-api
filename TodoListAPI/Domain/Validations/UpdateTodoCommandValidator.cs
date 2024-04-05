using FluentValidation;
using TodoListAPI.Domain.Command;

namespace TodoListAPI.Domain.Validations;

public class UpdateTodoCommandValidator: AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage(MessagesValidator.IdInvalid);
        RuleFor(p => p.UserId).NotEmpty().WithMessage(MessagesValidator.UserIdInvalid);
        RuleFor(p => p.Description).NotEmpty().WithMessage(MessagesValidator.DescriptionInvalid);
        RuleFor(p => p.CreatedAt).NotEmpty().WithMessage(MessagesValidator.CreatedAtInvalid);
    }
}