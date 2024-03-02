using System.Text;
using Core.Application.Helpers;
using Core.Infrastructure.Auth.Api;
using Core.Infrastructure.Auth.Api.Authenticated;
using Core.Infrastructure.Auth.Api.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Infrastructure.Auth;

internal static class Extensions
{
    public static IServiceCollection RegisterAuth(this IServiceCollection services)
    {
        var options = services.GetSettings<AuthenticationSettings>(AuthenticationSettings.SectionName);
        services.AddSingleton(options);

        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = options.JwtSettings.Issuer,
                    ValidAudience = options.JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.JwtSettings.IssuerSigningKey))
                };
            });

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, AuthenticatedRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

        return services;
    }
}