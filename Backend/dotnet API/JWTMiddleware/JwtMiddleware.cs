using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var tokenHeaderValue = context.Request.Headers["Authorization"];
        string token = string.Empty;

        if (!string.IsNullOrEmpty(tokenHeaderValue))
            token = tokenHeaderValue.ToString();


        if (!string.IsNullOrEmpty(token))
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                UserContext.UserId = int.Parse(userId);
            }
        }

        await _next(context);
    }
}
