using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IUserTokenAssociationRepository : IRepository<UserTokenAssociation>
    {
        public IQueryable<UserTokenAssociation> GetAll();
    }
}
