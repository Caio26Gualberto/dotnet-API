using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnet_API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Product> Produtos { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<SendMail> SendMails { get; set; }
        public DbSet<Email> Emails { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
    }
}
