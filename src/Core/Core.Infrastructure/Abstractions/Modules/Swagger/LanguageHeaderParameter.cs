using Core.Domain.Shared.Enumerations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using IOpenApiAny = Microsoft.OpenApi.Any.IOpenApiAny;
using OpenApiString = Microsoft.OpenApi.Any.OpenApiString;

namespace Core.Infrastructure.Abstractions.Modules.Swagger;

internal sealed class LanguageHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];
        operation.Parameters.Add(new OpenApiParameter
        {
            Required = false,
            Name = "X-Language",
            Description = "User Language",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = "String",
                Enum = Language.All.ToList().ConvertAll(value => (IOpenApiAny) new OpenApiString(value.Value)),
                Default = new OpenApiString(Language.Default)
            }
        });
    }
}