using dotnet_API.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_API.Services
{
    public class UserService
    {
        private readonly ApiContext _context;
        public UserService(ApiContext context)
        {
            _context = context;
        }
        public void CreateUser(User input)
        {
            input.DataRegistro = DateTime.Now;
            _context.Usuarios.Add(input);
            _context.SaveChanges();
        }

        public void DeleteUser(User input)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == input.Id);

            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }

            throw new Exception("Não foi possível achar um usuário");
        }

        public void UpdateUser(User input)
        {
            _context.Update(input);
        }
    }
}
