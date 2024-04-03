using TodoListAPI.Application.Requests;
using TodoListAPI.Domain.Validations;

namespace TodoListAPI.Test.Domain.Validations;

public class LoginUserRequestValidatorTest
{
    private readonly LoginUserRequestValidator _validator = new();

    [Fact(DisplayName = "Should be able to validate the object")]
    public void ShouldReturnOkInValidation()
    {
        var command = new LoginUserRequest
        {
            email = "teste@teste.com",
            password = "12345678",
        };

        var result = _validator.Validate(command);
        
        Assert.True(result.IsValid);
    }
    
    
    [Fact(DisplayName = "Should return email field error")]
    public void ShouldReturnEmailErrorInValidation()
    {
        var command = new LoginUserRequest
        {
            email = "teste",
            password = "12345678",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.email)));
    }

    [Fact(DisplayName = "Should return password field error")]
    public void ShouldReturnPasswordErrorInValidation()
    {
        var command = new LoginUserRequest
        {
            email = "teste@teste.com",
            password = "1",
        };

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, failure => failure.PropertyName.Equals(nameof(command.password)));
    }
}