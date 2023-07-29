using Microsoft.EntityFrameworkCore;
using static dotnet_API.Services.SpotifyService;

namespace dotnet_API.Models
{
    public class ANewLevelContext : DbContext
    {
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<User> Usuarios { get; set; }
        public virtual DbSet<ApiKey> ApiKeys { get; set; }
        public ANewLevelContext(DbContextOptions<ANewLevelContext> options) : base(options)
        {
        }
    }
}
