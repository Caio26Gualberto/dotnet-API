using dotnet_API.Services;
using Xunit;

namespace dotnetAPI.Tests.UnitTests
{
    public class AuthTests : BaseTest
    {
        [Theory]
        [InlineData("Caio12345", 1)]
        public async Task ShouldGenerateValidUrl(string email, int id)
        {
            var userService = GetService<UserService>();
            var url = userService.GenerateURI(email, id);
            Assert.NotNull(url);
        }
    }
}
