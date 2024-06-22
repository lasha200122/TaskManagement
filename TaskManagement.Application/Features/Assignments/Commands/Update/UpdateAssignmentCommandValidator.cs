namespace TaskManagement.Application.Features.Assignments.Commands.Update;

public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator() 
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.DueDate)
            .NotNull();

        RuleFor(x => x.Id)
            .NotNull();
    }
}
