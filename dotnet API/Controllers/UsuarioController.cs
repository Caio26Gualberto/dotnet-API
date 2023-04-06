﻿using dotnet_API.Dtos;
using dotnet_API.Models;
using dotnet_API.Repositories;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserService UserService;
        private readonly EmailService EmailService;
        private readonly ANewLevelContext Context;
        private readonly UserRepository _userRepository;

        private readonly EnvironmentVariable EVariable;

        public UsuarioController(UserService usuario, ANewLevelContext context, UserRepository usuarioRepository, EmailService emailService)
        {
            UserService = usuario;
            Context = context;
            _userRepository = usuarioRepository;
            EmailService = emailService;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> CreateUser(CreateUserDto input)
        {
            var isExistentAccount = _userRepository.GetAll()
                .Any(x => x.Email == input.Email || x.Login == input.Login);

            if (isExistentAccount)
                return BadRequest("Credencias já existentes em nosso sistema");

            User usuario = new User();
            CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);

            usuario.Name = input.Name;
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.Email = input.Email;
            usuario.BirthPlace = input.BirthPlace;
            usuario.Login = input.Login;
            usuario.Password = input.Password;

            UserService.CreateUser(usuario);

            return Ok(usuario);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsVerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.SerialNumber, user.Password)
            };

            var takeSecretKey = EVariable.JWTApiToken;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(takeSecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPost("/Login")]
        public async Task<ActionResult<string>> Login(CreateUserDto input)
        {
            var isNotAUser = _userRepository.GetAll()
                .Any(x => x.Email != input.Email || x.Login != input.Login);

            if (isNotAUser)
                return BadRequest("Usuário não encontrado");

            var usuario = _userRepository.GetAll()
                .Where(x => x.Email == input.Email || x.Login == input.Login)
                .FirstOrDefault();

            if (!IsVerifyPasswordHash(input.Password, usuario.PasswordHash, usuario.PasswordSalt))
                return BadRequest("Senha incorreta!");

            string token = CreateToken(usuario);
            return Ok("OKOKO");
        }

        [HttpPost("/Delete")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto input)
        {
            var user = Context.Usuarios.FirstOrDefault(x => x.Id == input.Id);
            if (user != null)
                UserService.DeleteUser(user);
            else
            {
                return NotFound("Não foi possível encontrar o usuário");
            }
            return Ok();
        }

        [HttpPost("/UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto input)
        {
            var user = Context.Usuarios.FirstOrDefault(x => x.Id == input.Id);
            if (user != null)
            {
                user.Email = input.Email;
                user.BirthPlace = input.LocalNascimento;
                user.Name = input.Nome;

                Context.Usuarios.Update(user);
                Context.SaveChanges();
            }
            return Ok();
        }

        [HttpGet("/GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var usuario = _userRepository.GetAll()
                .Where(x => x.Id == userId);

            return Ok(usuario);
        }

        [HttpPost("/ForgottenPassword")]
        public async Task<IActionResult> ResetPassword(int userId)
        {
            var userMail = _userRepository.GetAll()
                .Where(x => x.Id == userId)
                .Select(x => x.Email)
                .FirstOrDefault();

            await EmailService.SendResetPasswordEmail(userMail);

            return Ok();
        }
    }
}
