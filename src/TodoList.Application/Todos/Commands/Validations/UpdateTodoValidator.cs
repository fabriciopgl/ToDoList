using FluentValidation;

namespace ToDoList.Application.Todos.Commands.Validations;

public class UpdateTodoValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Todo title is required")
            .NotNull()
            .WithMessage("Todo title cannot be null")
            .MaximumLength(100)
            .WithMessage("Max lenght for title is 100");

        RuleFor(c => c.Description)
            .NotNull()
            .WithMessage("Todo description is required")
            .NotEmpty()
            .WithMessage("Todo description is required");
    }
}
