namespace TaskManagement.Application.Features.Authorization.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<ErrorOr<AuthorizationResponse>>;