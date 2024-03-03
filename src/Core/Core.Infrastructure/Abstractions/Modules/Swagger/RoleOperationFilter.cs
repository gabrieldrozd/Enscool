using Core.Infrastructure.Auth.Api.Roles;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Infrastructure.Abstractions.Modules.Swagger;

internal sealed class RoleOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var roleRequirements = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<RoleRequirementAttribute>()
            .ToList();

        var roleBaseRequirements = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<RoleRequirementBaseAttribute>()
            .ToList();

        if (roleRequirements.Count == 0 && roleBaseRequirements.Count == 0)
            return;

        string[] roles =
        [
            ..roleRequirements
                .SelectMany(x => x.Roles)
                .Select(x => x.ToString()),
            ..roleBaseRequirements
                .SelectMany(x => x.Roles)
                .Select(x => x.ToString())
        ];

        if (operation.Description is not null && operation.Description.Length != 0)
        {
            operation.Description += "\n\n";
            operation.Description += "----";
            operation.Description += "\n\n";
        }

        operation.Description += $"#### Alowed Roles:\n- {string.Join("\n- ", roles)}";
    }
}