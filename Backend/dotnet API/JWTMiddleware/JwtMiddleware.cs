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
        var token = context.Request.Cookies["Authorization"];

        if (token != null)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                context.Items["userId"] = userId;
            }
        }

        await _next(context);
    }
}
