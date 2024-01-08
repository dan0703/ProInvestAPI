using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProInvestAPI.Business;
using ProInvestAPI.Domain;
using ProInvestAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Add services to the container.
var key = configuration["JWT:Key"];
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowProInvestApp", builder =>
    {
        builder.WithOrigins("https://proinvestapi.azurewebsites.net")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });

    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});    
//MySqlConfiguration
builder.Services.AddDbContext<ProInvestDbContext>(options =>
                options.UseMySql(
                    "Server=viaduct.proxy.rlwy.net;Port=26597;Database=ProInvestDB;User=proInvest;Password=Pinv02@c34d;Protocol=TCP;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"
                )));

//add controllers
builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<InvestmentTypeProvider>();
builder.Services.AddScoped<BankProvider>();
builder.Services.AddScoped<DirectionProvider>();
builder.Services.AddScoped<OriginOfFoundsProvider>();
builder.Services.AddScoped<InvestmentRequestProvider>();
builder.Services.AddScoped<InvestmentSimulatorProvider>();
builder.Services.AddScoped<ClientProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
