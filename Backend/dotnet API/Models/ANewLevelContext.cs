using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static dotnet_API.Services.SpotifyService;

namespace dotnet_API.Models
{
    public class ANewLevelContext : IdentityDbContext
    {
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<User> Usuarios { get; set; }
        public virtual DbSet<ApiKey> ApiKeys { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<UserTokenAssociation> UserTokenAssociations { get; set; }
        public ANewLevelContext(DbContextOptions<ANewLevelContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTokenAssociation>()
                .HasIndex(e => e.RefreshToken)
                .IsUnique(); // Opcional, se você quiser um índice único

            base.OnModelCreating(modelBuilder);
        }
    }
}
