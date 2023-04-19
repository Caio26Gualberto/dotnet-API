using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; } 
    }
}
