using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.Services;
using ProtocolReception.Infrastructure.Repositories;
using ProtocolReception.Infrastructure.Repositories.Interfaces;
using System.Text;

namespace ProtocolReception.PublicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();
            // Add configuration from appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();


            builder.Services.AddEndpointsApiExplorer();
            // Swagger
            builder.Services.AddSwaggerGen();

            // Dababase
            builder.Services.AddDbContext<ProtocolContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                b => b.MigrationsAssembly("ProtocolReception.PublicApi")));

            // Injection
            builder.Services.AddScoped<IProtocolRepository, ProtocolRepository>();
            builder.Services.AddScoped<IProtocolLogRepository, ProtocolLogRepository>();
            // Domain Services
            builder.Services.AddScoped<ProtocolService>();
            builder.Services.AddScoped<ProtocolLogService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Testing API V1"); });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
            IConfiguration configuration = app.Configuration;
            IWebHostEnvironment environment = app.Environment;

            app.MapControllers();

            app.Run();
        }
    }
}
