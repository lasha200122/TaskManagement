namespace TaskManagement.Application.Common.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
    RefreshToken GenerateRefreshToken();
}
