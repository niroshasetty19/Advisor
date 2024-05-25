using AdvisorApp.Application.Interfaces;
using AdvisorApp.Application.Services;
using AdvisorApp.Domain.Interfaces;
using AdvisorApp.Infrastructure.Persistence;
using AdvisorApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Advisor.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register the DbContext
            builder.Services.AddDbContext<AdvisorDbContext>(options =>
                options.UseInMemoryDatabase("AdvisorDb"));

            // Register the repositories
            builder.Services.AddScoped<IAdvisorRepository, CachedAdvisorRepository>();

            // Register the application services
            builder.Services.AddScoped<IAdvisorService, AdvisorService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Get the allowed origins from the configuration
            var allowedOriginsString = builder.Configuration.GetSection("AllowedOrigins").Get<string>();
            var allowedOrigins = allowedOriginsString?.Split(',') ?? new[] { "http://localhost:4200" };

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins(allowedOrigins)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Enable CORS
            app.UseCors("AllowOrigin");

            app.MapControllers();

            app.Run();
        }
    }
}
