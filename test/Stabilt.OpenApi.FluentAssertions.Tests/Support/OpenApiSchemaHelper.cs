using Microsoft.OpenApi.Any;

namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiSchemaHelper
{
    public static OpenApiSchema Create() => new();

    public static OpenApiSchema CreateArray() => new()
    {
        Type = "array",
    };

    public static OpenApiSchema CreateBase64File() => new()
    {
        Type = "string",
        Format = "byte",
    };

    public static OpenApiSchema CreateBinaryFile() => new()
    {
        Type = "string",
        Format = "binary",
    };

    public static OpenApiSchema CreateEnum(params String[] values) =>
        CreateEnum(values.Select(x => new OpenApiString(x)).OfType<IOpenApiAny>().ToArray());

    public static OpenApiSchema CreateEnum(params IOpenApiAny[] values) => new()
    {
        Type = "string",
        Enum = values.ToList(),
    };

    public static OpenApiSchema CreateObject(String? title = null, String? referenceId = null) => new()
    {
        Type = "object",
        Title = title,
        Reference = referenceId != null ? new OpenApiReference { Id = referenceId } : null,
    };

    public static OpenApiSchema CreateObjectReference(String referenceId, String? title = null) => new()
    {
        Type = "object",
        Title = title ?? referenceId,
        Reference = new OpenApiReference
        {
            Id = referenceId,
        },
    };

    public static OpenApiSchema CreateType(String type, String? format = null) => new()
    {
        Type = type,
        Format = format,
        Description = null,
        Default = null,
        Not = null,
        AdditionalPropertiesAllowed = false,
        Discriminator = null,
        Example = null,
        ExternalDocs = null,
        Xml = null,
        Extensions = null,
        UnresolvedReference = false,
    };

    public static OpenApiSchema WithAdditionalProperties(this OpenApiSchema schema, OpenApiSchema additionalPropertiesSchema)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.AdditionalProperties = additionalPropertiesSchema;

        return schema;
    }

    public static OpenApiSchema WithAllOfReference(this OpenApiSchema schema, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.AllOf ??= new List<OpenApiSchema>();
        schema.AllOf.Add(new OpenApiSchema
        {
            Reference = new OpenApiReference
            {
                Id = referenceId,
            },
        });

        return schema;
    }

    public static OpenApiSchema WithAllOfType(this OpenApiSchema schema, String type)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.AllOf ??= new List<OpenApiSchema>();
        schema.AllOf.Add(new OpenApiSchema
        {
            Type = type,
        });

        return schema;
    }

    public static OpenApiSchema WithAnyOfReference(this OpenApiSchema schema, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.AnyOf ??= new List<OpenApiSchema>();
        schema.AnyOf.Add(new OpenApiSchema
        {
            Reference = new OpenApiReference
            {
                Id = referenceId,
            },
        });

        return schema;
    }

    public static OpenApiSchema WithAnyOfType(this OpenApiSchema schema, String type)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.AnyOf ??= new List<OpenApiSchema>();
        schema.AnyOf.Add(new OpenApiSchema
        {
            Type = type,
        });

        return schema;
    }

    public static OpenApiSchema WithDeprecated(this OpenApiSchema schema, Boolean deprecated = true)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Deprecated = deprecated;

        return schema;
    }

    public static OpenApiSchema WithItems(this OpenApiSchema schema, OpenApiSchema itemsSchema)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Items = itemsSchema;

        return schema;
    }

    public static OpenApiSchema WithMaximum(this OpenApiSchema schema, Decimal maximum)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Maximum = maximum;

        return schema;
    }

    public static OpenApiSchema WithMaximumExlusive(this OpenApiSchema schema, Decimal maximum)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Maximum = maximum;
        schema.ExclusiveMaximum = true;

        return schema;
    }

    public static OpenApiSchema WithMaxItems(this OpenApiSchema schema, Int32 maxItems)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MaxItems = maxItems;

        return schema;
    }

    public static OpenApiSchema WithMaxLength(this OpenApiSchema schema, Int32 maxLength)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MaxLength = maxLength;

        return schema;
    }

    public static OpenApiSchema WithMaxProperties(this OpenApiSchema schema, Int32 maxProperties)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MaxProperties = maxProperties;

        return schema;
    }

    public static OpenApiSchema WithMinimum(this OpenApiSchema schema, Decimal minimum)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Minimum = minimum;

        return schema;
    }

    public static OpenApiSchema WithMinimumExlusive(this OpenApiSchema schema, Decimal minimum)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Minimum = minimum;
        schema.ExclusiveMinimum = true;

        return schema;
    }

    public static OpenApiSchema WithMinItems(this OpenApiSchema schema, Int32 minItems)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MinItems = minItems;

        return schema;
    }

    public static OpenApiSchema WithMinLength(this OpenApiSchema schema, Int32 minLength)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MinLength = minLength;

        return schema;
    }

    public static OpenApiSchema WithMinProperties(this OpenApiSchema schema, Int32 minProperties)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MinProperties = minProperties;

        return schema;
    }

    public static OpenApiSchema WithMultipleOf(this OpenApiSchema schema, Decimal multipleOf)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.MultipleOf = multipleOf;

        return schema;
    }

    public static OpenApiSchema WithNullable(this OpenApiSchema schema, Boolean nullable = true)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Nullable = nullable;

        return schema;
    }

    public static OpenApiSchema WithOneOfReference(this OpenApiSchema schema, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.OneOf ??= new List<OpenApiSchema>();
        schema.OneOf.Add(new OpenApiSchema
        {
            Reference = new OpenApiReference
            {
                Id = referenceId,
            },
        });

        return schema;
    }

    public static OpenApiSchema WithOneOfType(this OpenApiSchema schema, String type)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.OneOf ??= new List<OpenApiSchema>();
        schema.OneOf.Add(new OpenApiSchema
        {
            Type = type,
        });

        return schema;
    }

    public static OpenApiSchema WithPattern(this OpenApiSchema schema, String pattern)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Pattern = pattern;

        return schema;
    }

    public static OpenApiSchema WithProperty(this OpenApiSchema schema, String name, OpenApiSchema property, Boolean required = false)
    {
        ArgumentNullException.ThrowIfNull(schema);

        if (required)
        {
            schema.Required ??= new HashSet<String>();
            schema.Required.Add(name);
        }

        schema.Properties ??= new Dictionary<String, OpenApiSchema>();
        schema.Properties[name] = property;

        return schema;
    }

    public static OpenApiSchema WithReadOnly(this OpenApiSchema schema, Boolean readOnly = true)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.ReadOnly = readOnly;

        return schema;
    }

    public static OpenApiSchema WithReference(this OpenApiSchema schema, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.Reference = new OpenApiReference
        {
            Id = referenceId,
        };

        return schema;
    }

    public static OpenApiSchema WithUniqueItems(this OpenApiSchema schema, Boolean uniqueItems = true)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.UniqueItems = uniqueItems;

        return schema;
    }

    public static OpenApiSchema WithWriteOnly(this OpenApiSchema schema, Boolean writeOnly = true)
    {
        ArgumentNullException.ThrowIfNull(schema);

        schema.WriteOnly = writeOnly;

        return schema;
    }
}
