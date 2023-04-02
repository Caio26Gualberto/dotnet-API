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
        private readonly UsuarioServico usuarioServico;
        private readonly ApiContext _context;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Email _email;

        public UsuarioController(UsuarioServico usuario, ApiContext context, UsuarioRepository usuarioRepository, Email email)
        {
            usuarioServico = usuario;
            _context = context;
            _usuarioRepository = usuarioRepository;
            _email = email;

        }

        [HttpPost("/CreateUser")]
        public IActionResult CreateUser(CreateUserDto input)
        {
            Usuario usuario = new Usuario();

            usuario.Nome = input.Nome;
            usuario.Email = input.Email;
            usuario.LocalNascimento = input.LocalNascimento;
            usuario.Login = input.Login;
            usuario.Senha = input.Senha;

            usuarioServico.CreateUser(usuario);

            return Ok();
        }

        [HttpPost("/DeleteUser")]
        public IActionResult DeleteUser(DeleteUserDto input)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == input.Id);
            usuarioServico.DeleteUser(user);

            return Ok();
        }

        [HttpPost("/UpdateUser")]
        public IActionResult UpdateUser(UpdateUserDto input)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == input.Id);
            if (user != null)
            {
                user.Email = input.Email;
                user.LocalNascimento = input.LocalNascimento;
                user.Nome = input.Nome;

                usuarioServico.UpdateUser(user);
            }
            return Ok();
        }

        [HttpGet("/GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var usuario = _usuarioRepository.GetAll()
                .Where(x => x.Id == userId);

            return Ok(usuario);
        }

        [HttpPost("/ForgottenPassword")]
        public async Task<IActionResult> ResetPassword(int userId)
        {
            SendMail s = new SendMail(_email);
            var userMail = _usuarioRepository.GetAll()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            s.SendEmail(userMail.Email);
            return Ok();
        }
    }
}
