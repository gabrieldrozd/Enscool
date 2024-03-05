using System.Reflection;
using Core.Infrastructure.Abstractions.Modules.Swagger.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Infrastructure.Abstractions.Modules.Swagger;

public sealed record XmlComments(List<string> Comments);

internal static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IList<Assembly> assemblies)
    {
        RegisterXmlComments(services, assemblies);

        services.AddSwaggerGen(swagger =>
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

            using var serviceProvider = services.BuildServiceProvider();
            var xmlComments = serviceProvider.GetRequiredService<XmlComments>();
            xmlComments.Comments.ForEach(xmlFile => swagger.IncludeXmlComments(xmlFile));

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
                options.SwaggerEndpoint($"/swagger/{group.Value}/swagger.json", $"{group.Key} API");
            }
        });

        return app;
    }

    private static IServiceCollection RegisterXmlComments(IServiceCollection services, IList<Assembly> assemblies)
    {
        const string modulePart = "Enscool.Modules.";
        const string commonPart = "Enscool.Core.Domain";
        const string searchPattern = "*.xml";

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var xmlFilesInDirectory = Directory.GetFiles(baseDirectory, searchPattern);

        var moduleXmlFiles = assemblies
            .Where(a => a.GetName().Name!.Contains(modulePart))
            .Select(a => xmlFilesInDirectory.FirstOrDefault(file => file.Contains($"{a.GetName().Name}.xml")))
            .Where(path => path != null);

        var commonXmlFile = xmlFilesInDirectory.FirstOrDefault(file => file.Contains($"{commonPart}.xml"));

        var xmlFiles = new List<string>(moduleXmlFiles!);
        if (commonXmlFile != null) xmlFiles.Add(commonXmlFile);
        services.AddSingleton(new XmlComments(xmlFiles));

        return services;
    }
}