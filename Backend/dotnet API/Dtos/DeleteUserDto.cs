using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Dtos
{
    public class DeleteUserDto
    {
        [Required]
        public int Id { get; set; }
    }
}
