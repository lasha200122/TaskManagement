namespace TaskManagement.Application.Features.Users.Commands.Register;

public sealed record RegisterUserCommand(
    string FullName,
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<DefaultResponse>>;