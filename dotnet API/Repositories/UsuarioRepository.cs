using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private readonly ApiContext _context;

        public UsuarioRepository(ApiContext context)
        {
            _context = context;
        }
        public IQueryable<Usuario> GetAll()
        {
            return _context.Usuarios.Where(x => true);
        }
    }
}
