namespace EIMS.Infrastructure.Security
{
    // This class is used to map the appsettings.json section
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Secret { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int AccessTokenExpirationMinutes { get; init; }
        public int RefreshTokenExpirationDays { get; init; }
    }
}