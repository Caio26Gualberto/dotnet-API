using dotnet_API.Dtos;
using dotnet_API.Entities;
using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IUserService
    {
        public void CreateUser(User input);
        public void DeleteUser(User user);
        public void UpdateUser(User input);
        public Task<UserManagerResponse> RegisterAccountAsync(CreateUserDto input);
        public Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        public Task<UserManagerResponse> ForgetPasswordAsync(string email);
        public Task<UserManagerResponse> ResetPasswordAsync(UpdatePasswordDto input);
        public Task<UserManagerResponse> LoginAsync(LoginDto input);
        public Task<Dictionary<string, UserManagerResponse>> CreateToken(User user);
        public Task<string> GenerateURI(string email, int id);
        public void GenerateNewPassword(User user, string password);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
