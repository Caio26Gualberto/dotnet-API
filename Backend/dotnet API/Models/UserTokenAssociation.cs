using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_API.Models
{
    [Table("UsuarioRefreshAssociacao")]
    public class UserTokenAssociation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
