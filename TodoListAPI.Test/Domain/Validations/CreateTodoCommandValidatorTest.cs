using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Validations;

namespace TodoListAPI.Test.Domain.Validations;

public class CreateTodoCommandValidatorTest
{
    public CreateTodoCommandValidator _validator;
    
    public CreateTodoCommandValidatorTest()
    {
        _validator = new CreateTodoCommandValidator();
    }
    
    [Fact(DisplayName = "Should be able to validate the object")]
    public void ShouldReturnOkInValidation()
    {
        var command = new CreateTodoCommand
        {
            UserId = "b6158b42-a89c-45ab-a3eb-b4753fe1bb9b",
            Description = "teste@teste.com",
        };

        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);
    }
    
    
    [Fact(DisplayName = "Should return userId field error")]
    public void ShouldReturnUserIdErrorInValidation()
    {
        var command = new CreateTodoCommand
        {
            UserId = "",
            Description = "Teste description",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.UserId)));
    }
    
    [Fact(DisplayName = "Should return description field error")]
    public void ShouldReturnDescriptionErrorInValidation()
    {
        var command = new CreateTodoCommand
        {
            UserId = "b6158b42-a89c-45ab-a3eb-b4753fe1bb9b",
            Description = "",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.Description)));
    }
}