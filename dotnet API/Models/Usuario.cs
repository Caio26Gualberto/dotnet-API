namespace dotnet_API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string? LocalNascimento { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
