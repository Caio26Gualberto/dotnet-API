using dotnet_API.Dtos;
using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IUserService
    {
        public void CreateUser(User input);
        public void DeleteUser(User user);
        public void UpdateUser(User input);
        public Task<User> CreateAccount(CreateUserDto input);
        public Task<string> CreateToken(User user);
        public Task<string> GenerateURI(string email);
        public void GenerateNewPassword(User user, string password);
    }
}
