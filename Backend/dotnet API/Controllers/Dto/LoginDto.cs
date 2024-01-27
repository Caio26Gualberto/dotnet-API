using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Controllers.Dto
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
