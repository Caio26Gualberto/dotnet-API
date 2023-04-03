using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Repositories
{
    public class UserRepository : IUsuarioRepository
    {
        private readonly ApiContext _context;

        public UserRepository(ApiContext context)
        {
            _context = context;
        }
        public IQueryable<User> GetAll()
        {
            return _context.Usuarios.Where(x => true);
        }
    }
}
