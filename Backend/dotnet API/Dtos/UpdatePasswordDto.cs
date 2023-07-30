using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Dtos
{
    public class UpdatePasswordDto
    {
        public int Id { get; set; }
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
        public string Password { get; set; }    
    }
}
