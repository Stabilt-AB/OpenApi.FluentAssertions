namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiComponentsHelper
{
    public static OpenApiComponents Create() => new()
    {
        Responses = null,
        Parameters = null,
        Examples = null,
        RequestBodies = null,
        Headers = null,
        Links = null,
        Callbacks = null,
    };

    public static OpenApiComponents WithSchema(this OpenApiComponents components, String key)
    {
        ArgumentNullException.ThrowIfNull(components);

        components.Schemas ??= new Dictionary<String, OpenApiSchema>();
        components.Schemas[key] = OpenApiSchemaHelper.Create();

        return components;
    }

    public static OpenApiComponents WithSecurityScheme(this OpenApiComponents components, String key, OpenApiSecurityScheme? scheme = null)
    {
        ArgumentNullException.ThrowIfNull(components);

        components.SecuritySchemes ??= new Dictionary<String, OpenApiSecurityScheme>();
        components.SecuritySchemes[key] = scheme ?? OpenApiSecuritySchemeHelper.Create();

        return components;
    }
}
