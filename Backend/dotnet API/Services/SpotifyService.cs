using dotnet_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace dotnet_API.Services
{
    public class SpotifyService
    {
        private readonly ANewLevelContext _context;
        private readonly EnvironmentVariable _environment;
        public SpotifyService(EnvironmentVariable environmentVariable, ANewLevelContext context)
        {
            _environment = environmentVariable;
            _context = context;
        }
        public async Task<string> GetAccessTokenSpotify()
        {
            ApiKey apiToken = new();
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

            apiToken.ExpirationTime = DateTime.Now;
            apiToken.TokenString = accessToken;
            _context.ApiKeys.Add(apiToken);
            await _context.SaveChangesAsync();

            return accessToken;
        }

        public async Task<string> SearchArtist(string artistName, string? accessToken)
        {
            HttpClient client = new HttpClient();
            var dbToken = _context.ApiKeys.FirstOrDefault();
            var currentTime = DateTime.Now;
            bool isApikeyExpires = dbToken.ExpirationTime.AddHours(1) <= currentTime;

            if (isApikeyExpires)
            {
                var apiToken = _context.ApiKeys.FirstOrDefault();
                _context.ApiKeys.Remove(apiToken);
                await _context.SaveChangesAsync();
            }

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            string url = $"https://api.spotify.com/v1/search?q={artistName}&type=artist";
            HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody.Contains("Only valid bearer authentication supported") || isApikeyExpires)
            {
                string newAccessToken = await GetAccessTokenSpotify();
                return await SearchArtist(artistName, newAccessToken);
            }

            SpotifyArtistObject artistObject = JsonConvert.DeserializeObject<SpotifyArtistObject>(responseBody);
            return responseBody;
        }

        public class Artists
        {
            public string Href { get; set; }
            public List<Item> Items { get; set; }
            public int Limit { get; set; }
            public string Next { get; set; }
            public int Offset { get; set; }
            public object Previous { get; set; }
            public int Total { get; set; }
        }

        public class ExternalUrls
        {
            public string Spotify { get; set; }
        }

        public class Followers
        {
            public object Href { get; set; }
            public int Total { get; set; }
        }

        public class Image
        {
            public int Height { get; set; }
            public string Url { get; set; }
            public int Width { get; set; }
        }

        public class Item
        {
            public ExternalUrls ExternalUrls { get; set; }
            public Followers Followers { get; set; }
            public List<string> Genres { get; set; }
            public string Href { get; set; }
            public string Id { get; set; }
            public List<Image> Images { get; set; }
            public string Name { get; set; }
            public int Popularity { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }
        }

        public class SpotifyArtistObject
        {
            public Artists Artists { get; set; }
        }

        public class ApiKey
        {
            public int Id { get; set; }
            public string TokenString { get; set; }
            public DateTime ExpirationTime { get; set; }
        }
    }
}
