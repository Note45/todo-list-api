using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Validations;

namespace TodoListAPI.Test.Domain.Validations;

public class UpdateTodoCommandValidatorTest
{
    public UpdateTodoCommandValidator _validator;
    
    public UpdateTodoCommandValidatorTest()
    {
        _validator = new UpdateTodoCommandValidator();
    }
    
    [Fact(DisplayName = "Should be able to validate the object")]
    public void ShouldReturnOkInValidation()
    {
        var command = new UpdateTodoCommand
        {
            Id= "ad53dfde-3e36-4d66-adf8-0d0646dc3434",
            UserId = "ad53dfde-3e36-4d66-adf8-0d064424245",
            Description = "Teste description",
            CreatedAt = "2024-04-05T11:31:17.346Z",
        };

        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);
    }
    
    [Fact(DisplayName = "Should return Id field error")]
    public void ShouldReturnIddErrorInValidation()
    {
        var command = new UpdateTodoCommand
        {
            Id= "",
            UserId = "ad53dfde-3e36-4d66-adf8-0d064424245",
            Description = "Teste description",
            CreatedAt = "2024-04-05T11:31:17.346Z",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.Id)));
    }
    
    [Fact(DisplayName = "Should return userId field error")]
    public void ShouldReturnUserIdErrorInValidation()
    {
        var command = new UpdateTodoCommand
        {
            Id= "ad53dfde-3e36-4d66-adf8-0d0646dc3434",
            UserId = "",
            Description = "Teste description",
            CreatedAt = "2024-04-05T11:31:17.346Z",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.UserId)));
    }
    
    [Fact(DisplayName = "Should return description field error")]
    public void ShouldReturnDescriptionErrorInValidation()
    {
        var command = new UpdateTodoCommand
        {
            Id= "ad53dfde-3e36-4d66-adf8-0d0646dc3434",
            UserId = "ad53dfde-3e36-4d66-adf8-0d064424245",
            Description = "",
            CreatedAt = "2024-04-05T11:31:17.346Z",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.Description)));
    }
    
    [Fact(DisplayName = "Should return createAt field error")]
    public void ShouldReturnCreatedAdErrorInValidation()
    {
        var command = new UpdateTodoCommand
        {
            Id= "ad53dfde-3e36-4d66-adf8-0d0646dc3434",
            UserId = "ad53dfde-3e36-4d66-adf8-0d064424245",
            Description = "Teste Description",
            CreatedAt = "",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.CreatedAt)));
    }
}