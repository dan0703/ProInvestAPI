using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProInvestAPI.Business;
using ProInvestAPI.Domain;
using ProInvestAPI.Models;
using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseStartup<Startup>() // Utiliza la clase Startup
                    .ConfigureServices((hostContext, services) =>
                    {
                        // Configuración adicional de servicios si es necesario
                        services.AddControllers();
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();

                        // Obtén el IConfiguration del hostContext
                        var configuration = hostContext.Configuration;

                        services.Configure<TwilioSettings>(configuration.GetSection("TwilioSettings"));
                        services.AddSingleton(configuration.Get<TwilioSettings>());

                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowProInvestApp", builder =>
                            {
                                builder.WithOrigins("http://ec2-3-137-140-200.us-east-2.compute.amazonaws.com:5039")
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                            });
                        });

                        services.AddDbContext<ProInvestDbContext>(options =>
                            options.UseMySql("Server=viaduct.proxy.rlwy.net;Port=26597;Database=ProInvestDB;User=proInvest;Password=Pinv02@c34d;Protocol=TCP;",
                                            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql")));

                        services.AddScoped<UserProvider>();
                        services.AddScoped<InvestmentTypeProvider>();
                        services.AddScoped<BankProvider>();
                        services.AddScoped<DirectionProvider>();
                        services.AddScoped<OriginOfFoundsProvider>();
                        services.AddScoped<InvestmentRequestProvider>();
                        services.AddScoped<InvestmentSimulatorProvider>();

                    })
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        // Configuración adicional si es necesario
                    });
            });
}
