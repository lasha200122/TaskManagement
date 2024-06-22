namespace TaskManagement.Application.Features.Users.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator() 
    {
        RuleFor(x => x.FullName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);
    }
}
