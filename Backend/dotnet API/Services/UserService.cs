using Dapper;
using dotnet_API.Controllers.Dto;
using dotnet_API.Entities;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace dotnet_API.Services
{
    public class UserService : IUserService
    {
        private readonly ANewLevelContext _context;
        private readonly EnvironmentVariable _environment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserTokenAssociation> _userTokenAssociation;
        private readonly IEmailService _emailService;
        public UserService(ANewLevelContext context, EnvironmentVariable environment, UserManager<IdentityUser> userManager,
            IRepository<User> repository, IEmailService emailService, IRepository<UserTokenAssociation> userTokenAssociation)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
            _userRepository = repository;
            _emailService = emailService;
            _userTokenAssociation = userTokenAssociation;
        }
        public void CreateUser(User input)
        {
            input.DataRecord = DateTime.Now;

            _context.Usuarios.Add(input);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Não foi possível deletar o usuário");
            }
        }

        public void UpdateUser(User input)
        {
            _context.Usuarios.Update(input);
            _context.SaveChanges();
        }

        public async Task<UserManagerResponse> RegisterAsync(CreateUserDto input)
        {
            if (input == null)
                throw new NullReferenceException("Campo está vazio!");

            if (input.Password != input.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "As senhas não são iguais",
                    IsSuccess = false
                };
            }
            var identityUser = new IdentityUser
            {

                Email = input.Email,
                UserName = input.Login,
            };
            var result = await _userManager.CreateAsync(identityUser, input.Password);

            CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User usuario = new User();

            usuario.Name = input.Name;
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.Email = input.Email;
            usuario.BirthPlace = input.BirthPlace;
            usuario.Login = input.Login;
            usuario.Password = input.Password;

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"https://localhost:7213/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _emailService.SendMailAsync(input.Email, url);
                CreateUser(usuario);
                return new UserManagerResponse
                {
                    Message = "Conta registrada!",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "Houve algum erro ao criar sua conta",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        public async Task<UserManagerResponse> LoginAsync(LoginDto input)
        {
            var user = _userRepository.GetAll().Where(x => x.Email == input.Login || x.Login == input.Login).FirstOrDefault();
            var identityUser = await _userManager.FindByEmailAsync(input.Login);

            if (identityUser == null)
                identityUser = await _userManager.FindByNameAsync(input.Login);

            if (user == null || identityUser == null)
            {
                return new UserManagerResponse
                {
                    Message = "Este email ou login não existe em nossa base de dados",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(identityUser, input.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Senha incorreta",
                    IsSuccess = false
                };

            var token = await CreateToken(user);
            var refreshToken = await RefreshTokenAsync(user.Id, true);

            UserTokenAssociation refreshTokenAssociation = new UserTokenAssociation()
            {
                UserId = user.Id,
                RefreshToken = refreshToken
            };

            _context.UserTokenAssociations.Add(refreshTokenAssociation);
            await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
                Message = "Bem-vindo!",
                Token = token.Keys.First(),
                RefreshToken = refreshToken,
                IsSuccess = true,
                FirstTimeLogin = user.FirstTimeLogin
            };
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<Dictionary<string, UserManagerResponse>> CreateToken(User user)
        {
            Dictionary<string, UserManagerResponse> dictionary = new Dictionary<string, UserManagerResponse>();
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_environment.JWTApiToken));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            dictionary.Add(tokenString, new UserManagerResponse
            {
                IsSuccess = true,
                Message = tokenString
            });

            return dictionary;
        }
        public async Task<string> GenerateURI(string email, int id)
        {
            string encryptedEmail = HttpUtility.UrlEncode(email);
            string redirectUrl = $"http://localhost:5500/ForgotPassword/forgotPassword.html?Email={encryptedEmail}&Id={id}";
            return redirectUrl;
        }

        public void GenerateNewPassword(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Password = password;
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _context.Usuarios.Update(user);
            _context.SaveChanges();
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Usuário não encontrado",
                    IsSuccess = false,
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmado com sucesso!",
                    IsSuccess = true
                };

            return new UserManagerResponse
            {
                Message = "Email não confirmado",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Nenhum usuário associado com este email",
                    IsSuccess = false
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"http://localhost:5500/ForgotPassword/forgotPassword.html?email={email}&token={validToken}";

            await _emailService.SendMailAsync(email, url);

            return new UserManagerResponse
            {
                Message = "Email para redefinição de senha enviado",
                IsSuccess = true
            };
        }

        public async Task<UserManagerResponse> ResetPasswordAsync(UpdatePasswordDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Nenhum usuário vinculado a este email",
                    IsSuccess = false
                };

            if (input.NewPassword != input.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "As senhas não coincidem",
                    IsSuccess = false
                };
            var decodedToken = WebEncoders.Base64UrlDecode(input.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, input.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Senha redefinida com sucesso!",
                    IsSuccess = true
                };

            return new UserManagerResponse
            {
                Message = "Alguma coisa deu errado, tente novamente mais tarde",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        public async Task<UserManagerResponse> ContinueToMainPageAsync(int userId)
        {
            try
            {
                var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == userId);
                user!.FirstTimeLogin = false;
                _context.Usuarios.Update(user);
                await _context.SaveChangesAsync();

                return new UserManagerResponse
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                throw e.InnerException!;
            }
        }

        public async Task<string> RefreshTokenAsync(int userId, bool isLogin = false)
        {
            IEnumerable<UserTokenAssociation>? tokenAssociation = null;
            if (!isLogin)
            {
                //_environment.LocalDb
                using (var connection = new SqlConnection("Server=DESKTOP-RFQMKVA;Database=NewLevel;Trusted_Connection=True;TrustServerCertificate=True;"))
                {
                    string query = "SELECT * FROM UsuarioRefreshAssociacao WHERE CAST(SUBSTRING(RefreshToken, CHARINDEX('-', RefreshToken) + 1, LEN(RefreshToken)) AS INT) = @UserId";

                    tokenAssociation = connection.Query<UserTokenAssociation>(query, new { UserId = userId });
                }

                return await GenerateRefreshToken(tokenAssociation.First().UserId);
            }
            else
            {
                return await GenerateRefreshToken(userId);
            }
        }

        private async Task<string> GenerateRefreshToken(int userId)
        {
            var randomBytes = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes) + ($"-{userId}");
        }
    }
}
