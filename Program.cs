using Microsoft.EntityFrameworkCore;
using ProInvestAPI.Business;
using ProInvestAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MySqlConfiguration
builder.Services.AddDbContext<ProInvestDbContext>(options =>
                options.UseMySql(
                    "Server=viaduct.proxy.rlwy.net;Port=26597;Database=ProInvestDB;User=proInvest;Password=Pinv02@c34d;Protocol=TCP;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"
                )));

//add controllers
builder.Services.AddScoped<UserProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
