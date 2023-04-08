using Newtonsoft.Json.Linq;
using System.Text;

namespace dotnet_API.Services
{
    public class SpotifyService
    {
        public async Task<string> GetAccessTokenSpotify(string clientId, string clientSecret)
        {
            HttpClient client = new HttpClient();
            string encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{"f9968a478d0249bc820ba9635b7efc70"}:{"ad174073a0c04cba9b1227becb3500fc"}"));
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
        public async Task<string> SearchArtist(string artistName, string accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "BQB19UFNHEQzkqyMmoV_tdXnz5uF4Tv7cZxeSeUAgxXXrjGZhTJ6UFythdYKk7zAxvtXqKep49EOpKgQP3YjeB87c-UiwjFPiGVS6pLaJZAHRzu1vNG1jZUrv33vXJnB7w_L");

            string url = $"https://api.spotify.com/v1/search?q={artistName}&type=artist";
            HttpResponseMessage response = await client.GetAsync(url);

            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
