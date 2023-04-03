using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IUsuarioRepository : IRepository<User>
    {
        public IQueryable<User> GetAll();
    }
}
