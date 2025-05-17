﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SuccessResponseSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsGenericType &&
            context.Type.GetGenericTypeDefinition().Name.StartsWith("SuccessResponse"))
        {
            if (schema.Properties.TryGetValue("error", out var errorSchema))
            {
                errorSchema.Example = new OpenApiNull();
                errorSchema.Nullable = true;
            }
        }
    }
}
