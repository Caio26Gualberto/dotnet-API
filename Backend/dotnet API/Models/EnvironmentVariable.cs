namespace dotnet_API.Models
{
    public class EnvironmentVariable
    {
        public string EmailLogin { get; set; } = Environment.GetEnvironmentVariable("NewLevelEmail");
        public string EmailPassword { get; set; } = Environment.GetEnvironmentVariable("NewLevelEmailPassword");
        public string JWTApiToken { get; set; } = Environment.GetEnvironmentVariable("SecretKey");
        public string SpotifyClientId { get; set; } = Environment.GetEnvironmentVariable("ClientIdSpotify");
        public string SpotifySecretId { get; set; } = Environment.GetEnvironmentVariable("ClientSecretSpotify");
        public string SendgridApiKey { get; set; } = Environment.GetEnvironmentVariable("SendgridApiKey");
        public string Localhost { get; set; } = Environment.GetEnvironmentVariable("Localhost");
    }
}
