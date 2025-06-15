using FluentValidation;

namespace TodoList.Application.Users.Commands.Validations;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}
