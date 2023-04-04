using dotnet_API.Dtos;
using dotnet_API.Models;
using dotnet_API.Repositories;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserService UserService;
        private readonly ANewLevelContext NewLevelContext;
        private readonly UserRepository UserRepository;
        private readonly SendMail SendMail;

        public UsuarioController(UserService usuario, ANewLevelContext context, UserRepository usuarioRepository, SendMail sendMail)
        {
            UserService = usuario;
            NewLevelContext = context;
            UserRepository = usuarioRepository;
            SendMail = sendMail;
        }

        [HttpPost("/CreateUser")]
        public IActionResult CreateUser(CreateUserDto input)
        {
            User usuario = new User();

            usuario.Nome = input.Nome;
            usuario.Email = input.Email;
            usuario.LocalNascimento = input.LocalNascimento;
            usuario.Login = input.Login;
            usuario.Senha = input.Senha;

            UserService.CreateUser(usuario);

            return Ok();
        }

        [HttpPost("/DeleteUser")]
        public IActionResult DeleteUser(DeleteUserDto input)
        {
            var user = NewLevelContext.Usuarios.FirstOrDefault(x => x.Id == input.Id);
            if (user != null)
                UserService.DeleteUser(user);
            else
            {
                return NotFound("Não foi possível encontrar o usuário");
            }


            return Ok();
        }

        [HttpPost("/UpdateUser")]
        public IActionResult UpdateUser(UpdateUserDto input)
        {
            var user = NewLevelContext.Usuarios.FirstOrDefault(x => x.Id == input.Id);
            if (user != null)
            {
                user.Email = input.Email;
                user.LocalNascimento = input.LocalNascimento;
                user.Nome = input.Nome;

                UserService.UpdateUser(user);
            }
            return Ok();
        }

        [HttpGet("/GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var usuario = UserRepository.GetAll()
                .Where(x => x.Id == userId);

            return Ok(usuario);
        }

        [HttpPost("/ForgottenPassword")]
        public async Task<IActionResult> ResetPassword(int userId)
        {
            var userMail = UserRepository.GetAll()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            SendMail email = new SendMail();
            var responseEmailBodyMessage = SendMail.SendEmail(userMail.Email, NewLevelContext);

            email.Body = await responseEmailBodyMessage;
            NewLevelContext.SendMails.Add(email);
            return Ok();
        }
    }
}
