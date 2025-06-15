using FluentValidation;

namespace TodoList.Application.Users.Commands.Validations;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .NotNull()
            .WithMessage("Name cannot be null")
            .MaximumLength(100)
            .WithMessage("Max length for name is 100");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(255)
            .WithMessage("Max length for email is 255");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters")
            .MaximumLength(100)
            .WithMessage("Max length for password is 100");
    }
}
