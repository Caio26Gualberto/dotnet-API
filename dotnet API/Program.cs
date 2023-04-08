using dotnet_API.Controllers;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using dotnet_API.Repositories;
using dotnet_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using System.Reflection.Metadata;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ANewLevelContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MainDb")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SecretKey"))),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization();

//Todo aprender nova forma de simplificar e organizar as injeções de dependências
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SpotifyService>();
builder.Services.AddScoped<IEmailService,EmailService>();
builder.Services.AddScoped<IArtistService,ArtistService>();
builder.Services.AddScoped<Artist>();
builder.Services.AddScoped<IUserRepository ,UserRepository>();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
