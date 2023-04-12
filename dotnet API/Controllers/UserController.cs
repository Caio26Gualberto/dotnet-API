using dotnet_API.Dtos;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using dotnet_API.Repositories;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        private readonly EnvironmentVariable _environment;

        public UserController(UserService usuario, IUserRepository usuarioRepository, IEmailService emailService, EnvironmentVariable environmentVariable)
        {
            _userService = usuario;
            _userRepository = usuarioRepository;
            _emailService = emailService;
            _environment = environmentVariable;

        }

        [AllowAnonymous]
        [HttpPost("/Register")]
        public async Task<IActionResult> Register(CreateUserDto input)
        {
            var isExistentAccount = _userRepository.GetAll()
                .Any(x => x.Email == input.Email || x.Login == input.Login);

            if (isExistentAccount)
                return BadRequest("Credencias já existentes em nosso sistema");

            var user = await _userService.CreateAccount(input);

            return Ok(user);
        }


        private bool IsVerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }



        [HttpPost("/Login")]
        public async Task<ActionResult<string>> Login(LoginDto input)
        {
            var loginUser = await _userRepository.GetAll().Where(x => x.Login == input.Login && x.Password == input.Password).FirstOrDefaultAsync();

            if (loginUser == null)
                return BadRequest("Usuário não encontrado");

            if (!IsVerifyPasswordHash(input.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
                return BadRequest("Senha incorreta!");

            string token = await _userService.CreateToken(loginUser);
            return Ok(token);
        }
        [Authorize]
        [HttpPost("/Delete")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto input)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == input.Id);

            if (user != null)
                _userService.DeleteUser(user);
            else
            {
                return NotFound("Não foi possível encontrar o usuário");
            }
            return Ok();
        }
        [Authorize]
        [HttpPost("/UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto input)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == input.Id);

            if (user != null)
            {
                user.Email = input.Email;
                user.BirthPlace = input.LocalNascimento;
                user.Name = input.Nome;

                _userService.UpdateUser(user);
            }
            return Ok();
        }

        [HttpGet("/GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var usuario = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == userId);

            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost("/ForgottenPassword")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var user = _userRepository.GetAll()
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (user == null)
                return BadRequest("Não existe este email em nossa base de dados");

            await _emailService.SendMailAsync(user.Email);

            return Ok();
        }
    }
}
