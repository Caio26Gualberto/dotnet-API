using dotnet_API.Dtos;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using Microsoft.IdentityModel.Tokens;
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
        public UserService(ANewLevelContext context, EnvironmentVariable environment)
        {
            _context = context;
            _environment = environment;
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

        public async Task<User> CreateAccount(CreateUserDto input)
        {
            CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User usuario = new User();

            usuario.Name = input.Name;
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.Email = input.Email;
            usuario.BirthPlace = input.BirthPlace;
            usuario.Login = input.Login;
            usuario.Password = input.Password;

            CreateUser(usuario);
            return usuario;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.SerialNumber, user.Password)
            };

            var takeSecretKey = _environment.JWTApiToken;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(takeSecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<string> GenerateURI(string email)
        {
            string emailCodificado = HttpUtility.UrlEncode(email);
            string urlDeRedirecionamento = $"http://localhost:5500/ForgotPassword/forgotPassword.html?Email={emailCodificado}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlDeRedirecionamento);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
            return urlDeRedirecionamento;
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
    }
}
