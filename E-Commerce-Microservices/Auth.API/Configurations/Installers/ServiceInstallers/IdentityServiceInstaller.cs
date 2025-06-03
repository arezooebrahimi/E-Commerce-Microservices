using Auth.API.Configurations.Installers;
using Auth.Data.Contexts;
using Auth.Models;
using Common.Attributes;
using Common.Entities.Auth;
using Common.Exceptions;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Auth.Configurations.Installers.ServiceInstallers
{
    [InstallerOrder(Order = 2)]
    public class IdentityServiceInstaller : IServiceInstaller
    {
        public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        string message = "Invalid token";
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        if (context.Exception is SecurityTokenExpiredException)
                            message = "Token expired";

                        var result = JsonSerializer.Serialize(new ApiResult(false, ApiResultStatusCode.UnAuthorized, message));
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            services.AddAuthorization();
            return Task.CompletedTask;
        }
    }
}
