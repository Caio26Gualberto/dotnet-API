using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnet_API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SendMail> SendMails { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
    }
}
