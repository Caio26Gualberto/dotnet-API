namespace dotnet_API.Models
{
    public class EnvironmentVariable
    {
        public string EmailLogin { get; set; } = Environment.GetEnvironmentVariable("NewLevelEmail");
        public string EmailPassword { get; set; } = Environment.GetEnvironmentVariable("NewLevelEmailPassword");
        public string JWTApiToken { get; set; } = Environment.GetEnvironmentVariable("SecretKey");
    }
}
