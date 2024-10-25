using Microsoft.EntityFrameworkCore;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.Services;
using ProtocolReception.Infrastructure.Repositories;
using ProtocolReception.Infrastructure.Repositories.Interfaces;
using ProtocolReception.PublicApi.Middleware;

namespace ProtocolReception.PublicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddEndpointsApiExplorer();

            // Swagger
            builder.Services.AddSwaggerGen();

            // Dababase
            builder.Services.AddDbContext<ProtocolContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                b => b.MigrationsAssembly("ProtocolReception.PublicApi")));

            // Dependency Injection
            builder.Services.AddScoped<IProtocolRepository, ProtocolRepository>();

            // Domain Services
            builder.Services.AddScoped<ProtocolService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Testing API V1"); });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
