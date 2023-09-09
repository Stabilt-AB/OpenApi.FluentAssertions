namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiParameterHelper
{
    public static OpenApiParameter Create(
        String name = "myParameter",
        ParameterLocation location = ParameterLocation.Query,
        Boolean required = false) => new()
    {
        Name = name,
        In = location,
        Required = required,
        Style = ParameterStyle.Simple,
        UnresolvedReference = false,
        Reference = null,
        Description = null,
        Explode = false,
        AllowReserved = false,
        Examples = null,
        Example = null,
        Content = null,
    };

    public static OpenApiParameter WithAllOfReference(this OpenApiParameter parameter, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        parameter.Schema ??= OpenApiSchemaHelper.Create();
        parameter.Schema.WithAllOfReference(referenceId);

        return parameter;
    }

    public static OpenApiParameter WithAllowEmptyValue(this OpenApiParameter operation, Boolean deprecated = true)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.AllowEmptyValue = deprecated;

        return operation;
    }

    public static OpenApiParameter WithAnyOf(this OpenApiParameter parameter, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        parameter.Schema ??= OpenApiSchemaHelper.Create();
        parameter.Schema.WithAnyOfReference(referenceId);

        return parameter;
    }

    public static OpenApiParameter WithDeprecated(this OpenApiParameter operation, Boolean deprecated = true)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Deprecated = deprecated;

        return operation;
    }

    public static OpenApiParameter WithEnumSchema(this OpenApiParameter parameter, params String[] values)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        parameter.Schema = OpenApiSchemaHelper.CreateEnum(values);

        return parameter;
    }

    public static OpenApiParameter WithOneOf(this OpenApiParameter parameter, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        parameter.Schema ??= OpenApiSchemaHelper.Create();
        parameter.Schema.WithOneOfReference(referenceId);

        return parameter;
    }

    public static OpenApiParameter WithReference(this OpenApiParameter parameter, String referenceId)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        parameter.Schema ??= OpenApiSchemaHelper.Create();
        parameter.Schema.WithReference(referenceId);

        return parameter;
    }

    public static OpenApiParameter WithSchema(this OpenApiParameter parameter, String type, String? format = null)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        parameter.Schema ??= new OpenApiSchema();
        parameter.Schema.Type = type;
        parameter.Schema.Format = format;

        return parameter;
    }
}
