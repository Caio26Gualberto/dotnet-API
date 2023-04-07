using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public IQueryable<User> GetAll();
    }
}
