using Ecommerce.Api.Data;
using Ecommerce.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
//                 CONFIGURACIÓN DE SERVICIOS
// ----------------------------------------------------

// Controllers (reemplaza el endpoint minimal /weatherforecast)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------
// PostgreSQL + EF Core
// ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------------
// Servicios propios
// ----------------------
builder.Services.AddScoped<AuthService>();

// ----------------------
// JWT Authentication
// ----------------------
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// ----------------------------------------------------
//                 CONFIGURACIÓN DE LA APP
// ----------------------------------------------------
var app = builder.Build();

// Swagger siempre visible (opcional: solo en desarrollo)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// JWT
app.UseAuthentication();
app.UseAuthorization();

// Controllers API
app.MapControllers();

app.Run();

