namespace TaskManagement.Application.Features.Assignments.Commands.Create;

public class CreateAssignmentCommandValidator : AbstractValidator<CreateAssignmentCommand>
{
    public CreateAssignmentCommandValidator() 
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.DueDate)
            .NotNull();
    }
}
