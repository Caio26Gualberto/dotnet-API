using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_API.Models
{
    [Table("Usuarios")]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Email { get; set; }
        public string? LocalNascimento { get; set; }
        public DateTime DataRegistro { get; set; }
        public virtual SendMail? SendMail { get; set; }
    }
}
