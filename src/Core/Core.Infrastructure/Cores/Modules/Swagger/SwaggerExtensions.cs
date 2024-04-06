using Core.Infrastructure.Cores.Modules.Swagger.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Infrastructure.Cores.Modules.Swagger;

internal static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services
            .AddSwaggerGen(swagger =>
            {
                foreach (var group in ApiGroups.GetNameValueDictionary())
                {
                    swagger.SwaggerDoc(
                        group.Value,
                        new OpenApiInfo
                        {
                            Title = $"{group.Key}",
                            Version = group.Value
                        });
                }

                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Description = "Raw JWT Bearer token",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                var requirement = new OpenApiSecurityRequirement { { securityScheme, new List<string>() } };
                swagger.AddSecurityRequirement(requirement);

                swagger.OperationFilter<LanguageHeaderParameter>();
                swagger.OperationFilter<RoleOperationFilter>();
            });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "";
            options.DocumentTitle = "Enscool API";

            foreach (var group in ApiGroups.GetNameValueDictionary())
            {
                options.SwaggerEndpoint($"/swagger/{group.Value}/swagger.json", $"Enscool {group.Key} API");
            }
        });

        return app;
    }
}