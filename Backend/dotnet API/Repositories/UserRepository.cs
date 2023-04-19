using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ANewLevelContext _context;

        public UserRepository(ANewLevelContext context)
        {
            _context = context;
        }
        public IQueryable<User> GetAll()
        {
            return _context.Usuarios.Where(x => true);
        }
    }
}
