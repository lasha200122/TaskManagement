namespace TaskManagement.Application.Common.Interfaces.Services;

public interface IAuthorizationService
{
    Task<ErrorOr<AuthorizationResponse>> Login(User user, string password);
    Task<bool> LogOut(Guid userId);
    Task<ErrorOr<AuthorizationResponse>> UpdateTokens(string accessToken, string refreshToken, User user);
}
