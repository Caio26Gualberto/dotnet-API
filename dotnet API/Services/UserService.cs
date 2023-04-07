using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Services
{
    public class UserService : IUserService
    {
        private readonly ANewLevelContext _context;
        public UserService(ANewLevelContext context)
        {
            _context = context;
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
    }
}
