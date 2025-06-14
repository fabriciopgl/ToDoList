using FluentValidation;

namespace ToDoList.Application.Tasks.Commands.Validations;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Task title is required")
            .NotNull()
            .WithMessage("Task title cannot be null")
            .MaximumLength(100)
            .WithMessage("Max lenght for title is 100");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Task description is required");

        RuleFor(c => c.DueDate)
            .Must(c => c.Date >= DateTime.Today)
            .WithMessage("Due date must be in the future or today");
    }
}
