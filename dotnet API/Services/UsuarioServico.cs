using dotnet_API.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_API.Services
{
    public class UsuarioServico
    {
        private readonly ApiContext _context;
        public UsuarioServico(ApiContext context)
        {
            _context = context;
        }
        public void CreateUser(Usuario input)
        {
            input.DataRegistro = DateTime.Now;
            _context.Usuarios.Add(input);
            _context.SaveChanges();
        }

        public void DeleteUser(Usuario input)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == input.Id);

            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }

            throw new Exception("Não foi possível achar um usuário");
        }

        public void UpdateUser(Usuario input)
        {
            _context.Update(input);
        }
    }
}
