using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IArtistService
    {
        public void CreateUser(User input);
        public void DeleteUser(User user);
        public void UpdateUser(User input);
        public Task<string> SearchArtist(string artistName, string accessToken);
    }
}
