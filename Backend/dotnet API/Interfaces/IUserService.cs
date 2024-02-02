using dotnet_API.Controllers.Dto;
using dotnet_API.Entities;
using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IUserService
    {
        public void CreateUser(User input);
        public void DeleteUser(User user);
        public void UpdateUser(User input);
        public Task<UserManagerResponse> RegisterAsync(CreateUserDto input);
        public Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        public Task<UserManagerResponse> ForgetPasswordAsync(string email);
        public Task<UserManagerResponse> ResetPasswordAsync(UpdatePasswordDto input);
        public Task<UserManagerResponse> LoginAsync(LoginDto input);
        public Task<string> CreateToken(int userId);
        public Task<string> GenerateURI(string email, int id);
        public void GenerateNewPassword(User user, string password);
        public Task<UserManagerResponse> ContinueToMainPageAsync();
        public Task<string> RefreshTokenAsync(int userId, bool forceRefreshToken = false, User user = null);
    }
}
