using dotnet_API.Interfaces;
using dotnet_API.Models;
using dotnet_API.Repositories;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ANewLevelContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MainDb")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ANewLevelContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SecretKey"))),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        // Permite qualquer origem. Substitua "*" pelo domínio do seu aplicativo em produção.
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

//Todo aprender nova forma de simplificar e organizar as injeções de dependências
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SpotifyService>();
builder.Services.AddScoped<EnvironmentVariable>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<Artist>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    if (token != null)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;

        // Obtenha o ID do usuário do token (supondo que o ID seja uma reivindicação no token)
        var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        // Adicione o ID do usuário ao contexto da solicitação
        if (!string.IsNullOrEmpty(userId))
            context.Items["UserId"] = userId;
    }

    await next();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
