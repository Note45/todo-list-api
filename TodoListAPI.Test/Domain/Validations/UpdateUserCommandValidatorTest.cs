using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Validations;

namespace TodoListAPI.Test.Domain.Validations;

public class UpdateUserCommandValidatorTest
{
    private readonly UpdateUserCommandValidator _validator = new();

    [Fact(DisplayName = "Should be able to validate the object")]
    public void ShouldReturnOkInValidation()
    {
        var command = new UpdateUserCommand
        {
            Name = "Teste",
            Email = "teste@teste.com",
            Password = "12345678",
        };

        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);
    }
    
    
    [Fact(DisplayName = "Should return email field error")]
    public void ShouldReturnEmailErrorInValidation()
    {
        var command = new UpdateUserCommand
        {
            Name = "Teste",
            Email = "teste",
            Password = "12345678",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.Email)));
    }
    
    [Fact(DisplayName = "Should return password field error")]
    public void ShouldReturnPasswordErrorInValidation()
    {
        var command = new UpdateUserCommand
        {
            Name = "Teste",
            Email = "teste@teste.com",
            Password = "1",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.Password)));
    }
    
    [Fact(DisplayName = "Should return name field error")]
    public void ShouldReturnNameErrorInValidation()
    {
        var command = new UpdateUserCommand
        {
            Name = "",
            Email = "teste@teste.com",
            Password = "12345678",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.Name)));
    }
}