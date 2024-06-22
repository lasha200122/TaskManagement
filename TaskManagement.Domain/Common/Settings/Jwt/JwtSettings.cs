namespace TaskManagement.Domain.Common.Settings.Jwt;

public class JwtSettings
{
    public string Secret { get; set; } = null!;
    public double ExpiryMinutes { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int RefreshTokenValidityInDays { get; set; }
}
