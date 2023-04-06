using Microsoft.EntityFrameworkCore;

namespace dotnet_API.Models
{
    public class ANewLevelContext : DbContext
    {
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<User> Usuarios { get; set; }
        public ANewLevelContext(DbContextOptions<ANewLevelContext> options) : base(options)
        {
        }
    }
}
