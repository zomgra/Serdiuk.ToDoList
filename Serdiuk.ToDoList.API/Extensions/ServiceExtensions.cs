using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serdiuk.ToDoList.Application.Common.Interfaces;
using Serdiuk.ToDoList.Application.Services;
using Serdiuk.ToDoList.Persistance;
using System.Text;

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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = config["Authentication:JWT:Issuer"],
                        ValidAudience = config["Authentication:JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Authentication:JWT:SecurityKey"]))
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
        public static void ConfigureSwagger(this IServiceCollection services, ILogger logger, IConfiguration configuration)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "This is Todo List API",
                    Version = "1",
                    Title = "TODO API",
                });


                var url = configuration.GetValue<string>("Authentication:JWT:Url");
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{url}Account/Login", UriKind.Absolute),
                            AuthorizationUrl = new Uri($"{url}Account/Login", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { configuration.GetValue<string>("Authentication:JWT:Audience"), "Todo List Api" }
                            },
                        }
                    },
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                }
            );
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
