namespace TaskManagement.Application.Features.Authorization.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ErrorOr<AuthorizationResponse>>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserRepository _userRepository;

    public LoginHandler(IAuthorizationService authorizationService, IUserRepository userRepository)
    {
        _authorizationService = authorizationService;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthorizationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetSingleOrDefaultAsync(x => x.Email == request.Email.ToLower()) is not User user)
            return Error.NotFound(description:"Email or Password is invalid");

        return await _authorizationService.Login(user, request.Password);
    }
}