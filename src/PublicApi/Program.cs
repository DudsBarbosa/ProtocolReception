using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.UseCases;
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
            builder.Services.AddSwaggerGen(options => options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }));
            builder.Services.AddSwaggerGen(options => options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            }));

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
