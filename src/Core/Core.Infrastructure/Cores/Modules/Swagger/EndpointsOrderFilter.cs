using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Infrastructure.Cores.Modules.Swagger;

internal sealed class EndpointsOrderFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths
            .OrderBy(p => p.Key)
            .ToList();

        swaggerDoc.Paths.Clear();
        paths.ForEach(p => swaggerDoc.Paths.Add(p.Key, p.Value));
    }
}