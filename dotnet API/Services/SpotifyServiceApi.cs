namespace dotnet_API.Services
{
    public class SpotifyServiceApi
    {
        public async Task<string> SearchArtist(string artistName, string accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            string url = $"https://api.spotify.com/v1/search?q={artistName}&type=artist";
            HttpResponseMessage response = await client.GetAsync(url);

            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
