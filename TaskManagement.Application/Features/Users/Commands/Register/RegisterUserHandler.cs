namespace TaskManagement.Application.Features.Users.Commands.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ErrorOr<DefaultResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.AnyAsync(x => x.Email == request.Email.ToLower())) 
            return Error.Conflict(description: "Email should be unique");

        string passwordHash = _passwordService.HashPasword(request.Password, out var passwordSalt);

        var user = new User(
            Guid.NewGuid(),
            request.FullName,
            request.Email.ToLower(),
            passwordHash,
            Convert.ToHexString(passwordSalt));

        _userRepository.Create(user);

        await _userRepository.SaveChanges(cancellationToken);

        return new DefaultResponse(true, nameof(RegisterUserHandler));
    }
}
