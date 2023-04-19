using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface ISendMail
    {
        public Task<string> SendEmail(string userEmail, ANewLevelContext context);
    }
}
