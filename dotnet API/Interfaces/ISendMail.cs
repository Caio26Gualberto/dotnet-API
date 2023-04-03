using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface ISendMail
    {
        public void SendEmail(string userEmail, ApiContext context);
    }
}
