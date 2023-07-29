using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IRepository<Entity>
    {
        public IQueryable<Entity> GetAll();
    }
}
