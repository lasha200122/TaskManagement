namespace TaskManagement.Application.Features.Authorization.Commands.Logout;

public sealed record LogoutCommand(Guid Id) : IRequest<ErrorOr<DefaultResponse>>;