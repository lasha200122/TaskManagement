namespace TaskManagement.Application.Features.Authorization.Commands.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, ErrorOr<DefaultResponse>>
{
    private readonly IAuthorizationService _authorizationService;

    public LogoutHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return new DefaultResponse(
            await _authorizationService.LogOut(request.Id),
            nameof(LogoutHandler));
    }
}
