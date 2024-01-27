using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Repositories
{
    public class UserTokenAssociationRepository : IUserTokenAssociationRepository
    {
        private readonly ANewLevelContext _context;

        public UserTokenAssociationRepository(ANewLevelContext context)
        {
            _context = context;
        }
        public IQueryable<UserTokenAssociation> GetAll()
        {
            return _context.UserTokenAssociations.Where(x => true);
        }
    }
}
