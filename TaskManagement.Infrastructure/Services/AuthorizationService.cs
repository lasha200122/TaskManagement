namespace TaskManagement.Infrastructure.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly AuthorizationSettings _settings;
    private readonly SessionManager<RefreshToken> _authorizationUserSessionManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordService _passwordService;

    public AuthorizationService(
        IRedisManager redis,
        AuthorizationSettings settings,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordService passwordService)
    {
        _settings = settings;
        _authorizationUserSessionManager = new SessionManager<RefreshToken>(redis);
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordService = passwordService;
    }

    public async Task<ErrorOr<AuthorizationResponse>> Login(User user, string password)
    {
        var validateResult = Validate(user, password);

        if (validateResult.IsError) return validateResult.Errors;

        string accessToken = _jwtTokenGenerator.GenerateJwtToken(user);

        RefreshToken refreshTokenData = _jwtTokenGenerator.GenerateRefreshToken();

        await _authorizationUserSessionManager.SetSessionAsync($"{_settings.SessionIdPrefix}_{user.Id}", refreshTokenData);

        AuthorizationResponse result = new(accessToken, refreshTokenData.Token);

        return result;
    }

    private ErrorOr<bool> Validate(User user, string password)
    {
        if (!_passwordService.VerifyPassword(password, user.PasswordHash, Convert.FromHexString(user.PasswordSalt)))
            return Error.Validation(code: "User Login", description: "Email or Password is incorrect");

        return true;
    }

    public async Task<bool> LogOut(Guid userId)
    {
        var key = $"{_settings.SessionIdPrefix}_{userId}";

        return await _authorizationUserSessionManager.DeleteSessionInfoAsync(key);
    }

    public async Task<ErrorOr<AuthorizationResponse>> UpdateTokens(string accessToken, string refreshToken, User user)
    {
        if (await _authorizationUserSessionManager.GetSessionAsync($"{_settings.SessionIdPrefix}_{user.Id}") is not RefreshToken refreshTokenData)
            return Error.NotFound(code: "Session", description: "User session expired");

        if (string.IsNullOrEmpty(refreshTokenData.Token) ||
            refreshTokenData.Token != refreshToken ||
            refreshTokenData.Expires <= DateTimeOffset.Now)
            return Error.Validation(code: "Refresh token", description: "Refresh token not valid.");

        string newAccessToken = _jwtTokenGenerator.GenerateJwtToken(user);

        RefreshToken newRefreshTokenData = _jwtTokenGenerator.GenerateRefreshToken();

        await _authorizationUserSessionManager.UpdateSessionAsync($"{_settings.SessionIdPrefix}_{user.Id}", newRefreshTokenData);

        AuthorizationResponse result = new(newAccessToken, newRefreshTokenData.Token);

        return result;
    }
}
