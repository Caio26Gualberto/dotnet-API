using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_API.Models
{
    [Table("Produtos")]
    public class Product
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public virtual User User { get; set; } 
    }
}
