using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Dtos
{
    public class CreateUserDto
    {
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Email { get; set; }
        public string? LocalNascimento { get; set; }
    }
}
