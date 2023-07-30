using dotnet_API.Dtos;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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

        public UserController(UserService userService, IUserRepository userRepository, IEmailService emailService, EnvironmentVariable environment)
        {
            _userService = userService;
            _userRepository = userRepository;
            _emailService = emailService;
            _environment = environment;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserDto input)
        {
            var isExistentAccount = _userRepository.GetAll()
                .Any(x => x.Email == input.Email || x.Login == input.Login);

            if (isExistentAccount)
                return BadRequest("Credenciais já existentes em nosso sistema");

            var user = await _userService.CreateAccount(input);

            return Ok("Registro feito com sucesso!");
        }


        private bool IsVerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDto input)
        {
            var loginUser = await _userRepository.GetAll()
                .Where(x => x.Login == input.Login || x.Email == input.Login)
                .FirstOrDefaultAsync();

            if (loginUser == null)
                return BadRequest("Usuário não encontrado");

            if (!IsVerifyPasswordHash(input.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
                return BadRequest("Senha incorreta");

            string token = await _userService.CreateToken(loginUser);
            return Ok("Bem-vindo!");
        }
        [Authorize]
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto input)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == input.Id);

            if (user != null)
                _userService.DeleteUser(user);
            else
                return NotFound("Não foi possível encontrar o usuário");

            return Ok();
        }
        [Authorize]
        [HttpPost("UpdateUser")]
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

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var usuario = _userRepository.GetAll().FirstOrDefault(x => x.Id == userId);

            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost("ForgottenPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = _userRepository.GetAll()
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (user == null)
                return BadRequest("Não existe este email em nossa base de dados");

            var uri = await _userService.GenerateURI(email);
            await _emailService.SendMailAsync(user.Email, uri);

            return Ok("Email com redefinição enviado para o email");
        }

        [HttpGet("RetrievingEmailFromToken")]
        public async Task<IActionResult> RecoverAccount([FromQuery] string email)
        {
            var userFromEmail = _userRepository.GetAll()
                .Where(x => x.Email == email)
                .FirstOrDefault();

            string url = "http://localhost:5500/ForgotPassword/forgotPassword.html";
            return Redirect(url);
        }

        [HttpPost("GenerateNewPassword")]
        public async Task<IActionResult> GenerateNewPassoword(string password, string email)
        {
            var user = _userRepository.GetAll()
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (user.Password == password)
                return BadRequest("Sua senha deve ser diferente da antiga");

            _userService.GenerateNewPassword(user, password);
            return Ok("Senha atualizada!");
        }
    }
}
