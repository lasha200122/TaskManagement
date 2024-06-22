namespace TaskManagement.Domain.Common.Models.Authorization;

public record RefreshToken(string Token, DateTimeOffset Expires);
