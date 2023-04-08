using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Services
{
    public class ArtistService : IArtistService
    {
        private readonly SpotifyService _spotify;
        public ArtistService(SpotifyService spotify)
        {
            _spotify = spotify;
        }
        public void CreateUser(User input)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User input)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SearchArtist(string artistName, string accessToken)
        {
            _spotify.GetAccessTokenSpotify("sa", "sas");
            var a = _spotify.SearchArtist("Slayer", "f9968a478d0249bc820ba9635b7efc70");
            return await a;
        }
    }
}
