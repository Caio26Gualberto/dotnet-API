using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? LocalNascimento { get; set; }
    }
}
