using dotnet_API.Interfaces;
using dotnet_API.Models;
using dotnet_API.Repositories;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
app.UseAuthorization();

app.MapControllers();

app.Run();
