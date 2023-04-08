using dotnet_API.Models;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text;

namespace dotnet_API.Services
{
    public class SpotifyService
    {
        private readonly EnvironmentVariable _environment;
        public SpotifyService()
        {

        }
        public async Task<string> GetAccessTokenSpotify()
        {
            HttpClient client = new HttpClient();
            string encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_environment.SpotifyClientId}:{_environment.SpotifySecretId}"));
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encodedAuth);

            string url = "https://accounts.spotify.com/api/token";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        {"grant_type", "client_credentials"},
        {"scope", "user-read-private user-read-email"}
    });
            HttpResponseMessage response = await client.PostAsync(url, content);

            string responseString = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(responseString);
            string accessToken = result.GetValue("access_token").ToString();
            return accessToken;
        }
        public async Task<string> SearchArtist(string artistName, string? accesToken)
        {
            var token = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesToken);

            string url = $"https://api.spotify.com/v1/search?q={artistName}&type=artist";
            HttpResponseMessage response = await client.GetAsync(url);
            string responsebody = await response.Content.ReadAsStringAsync();

            string responseString = await response.Content.ReadAsStringAsync();

            if (responsebody.Contains("Only valid bearer authentication supported"))
            {
                token = await GetAccessTokenSpotify();
                var artistInfo = await SearchArtist(artistName, token);
                return artistInfo;
            }

            return responseString;
        }
    }
}
