using FluentValidation;

namespace TodoList.Application.Users.Commands.Validations;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
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
    }
}
