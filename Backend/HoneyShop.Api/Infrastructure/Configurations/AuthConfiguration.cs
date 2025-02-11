using System.Text;
using HoneyShop.BusinessLogic.Auth;
using HoneyShop.Core.Constant;
using HoneyShop.Core.Contracts.Auth;
using HoneyShop.Model.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HoneyShop.Infrastructure.Configurations;

public static class AuthConfiguration
{
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration,
        AppSettings appSettings)
    {
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.JwtSettings.Issuer,
                        ValidAudience = appSettings.JwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSettings.SecretKey))
                    };
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Query["access_token"];

                        if (!String.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("A", 
                policy => policy.RequireRole(AuthConstant.Admin));
            options.AddPolicy("AM", 
                policy => policy.RequireRole(AuthConstant.Admin, AuthConstant.Manager));
            options.AddPolicy("AMU", 
                policy => policy.RequireRole(AuthConstant.Admin, AuthConstant.Manager, AuthConstant.User));
        });

      
    }
}