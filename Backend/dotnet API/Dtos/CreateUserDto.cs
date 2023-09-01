using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; }
        public string? BirthPlace { get; set; }
    }
}
