using dotnet_API.Interfaces;
using dotnet_API.Models;

namespace dotnet_API.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ANewLevelContext _context;
        public ArtistRepository(ANewLevelContext context) 
        { 
            _context = context;
        }
        public IQueryable<Artist> GetAll()
        {
           return _context.Artists.Where(x => true);
        }
    }
}
