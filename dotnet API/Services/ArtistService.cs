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

        public async Task<string> SearchArtist(string artistName)
        {
            var artist = await _spotify.SearchArtist(artistName, null);
            return artist;
        }
    }
}
