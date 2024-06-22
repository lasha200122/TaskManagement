namespace TaskManagement.Application.Features.Authorization.Common;

public sealed record AuthorizationResponse(string AccessToken, string RefreshToken);