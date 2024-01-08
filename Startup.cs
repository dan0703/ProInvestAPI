public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
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

        // Otras configuraciones de servicios si es necesario
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
            });
        }

        app.UseCors(); // Usa todas las políticas CORS configuradas
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
