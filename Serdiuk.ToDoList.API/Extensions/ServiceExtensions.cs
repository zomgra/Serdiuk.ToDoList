using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serdiuk.ToDoList.Application.Common.Interfaces;
using Serdiuk.ToDoList.Application.Services;
using Serdiuk.ToDoList.Persistance;

namespace Serdiuk.ToDoList.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureEntityFramework(this IServiceCollection services, ILogger logger)
        {
            services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("APP_DEV"));
            services.AddTransient<IAppDbContext, AppDbContext>();   
            logger.LogInformation("Configured EF Core.");
        }
        public static void ConfigureJWTBearer(this IServiceCollection services, ILogger logger, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Authentication:JWT:Issuer"],
                        ValidAudience = config["Authentication:JWT:Audience"],
                    };
                });
            services.AddAuthorization();
            logger.LogInformation("Configured JWT authentication scheme.");
        }
        public static void ConfigureIdentity(this IServiceCollection services, ILogger logger)
        {
            services.AddDbContext<AuthDbContext>(config =>
            {
                config.UseInMemoryDatabase("MEMORY");
            })
                .AddIdentity<IdentityUser, IdentityRole>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            
            logger.LogInformation("Configured Identity service.");
        }
        public static void ConfigureSwagger(this IServiceCollection services, ILogger logger)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Todo List API",
                    Version = "v1",

                });
            });
            logger.LogInformation("Configured Swagger.");
        }
        public static void ConfigureRepository(this IServiceCollection services, ILogger logger)
        {
            services.AddScoped<IToDoItemService, ToDoItemService>();
            logger.LogInformation("Configured Repository services.");
        }
    }
}
