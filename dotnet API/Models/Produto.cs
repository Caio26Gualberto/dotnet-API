namespace dotnet_API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
