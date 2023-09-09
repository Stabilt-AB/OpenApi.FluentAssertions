namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiDocumentHelper
{
    public static OpenApiDocument Create() => new()
    {
        Info = new OpenApiInfo
        {
            Title = "Test API",
            Description = "Specification for testing purposes",
            Version = "1.33.7",
            TermsOfService = new Uri("https://test.stabilt.dev/terms"),
            Contact = new OpenApiContact
            {
                Name = "Test Contact",
                Url = new Uri("https://test.stabilt.dev/contact"),
                Email = "contact@test.stabilt.dev",
            },
            License = new OpenApiLicense
            {
                Name = "Test license",
                Url = new Uri("https://test.stabilt.dev/license"),
            },
        },
        Tags = null,
        ExternalDocs = null,
    };

    public static OpenApiDocument WithComponents(this OpenApiDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        document.Components ??= OpenApiComponentsHelper.Create();

        return document;
    }

    public static OpenApiDocument WithPathItem(this OpenApiDocument document, String key) =>
        document.WithPathItem(key, _ => { });

    public static OpenApiDocument WithPathItem(this OpenApiDocument document, String key, Action<OpenApiPathItem> configurePath)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(configurePath);

        document.Paths ??= new OpenApiPaths();
        document.Paths[key] = OpenApiPathItemHelper.Create();

        configurePath(document.Paths[key]);

        return document;
    }

    public static OpenApiDocument WithSchemaComponent(this OpenApiDocument document, String key)
    {
        document
            .WithComponents();

        document.Components
            .WithSchema(key);

        return document;
    }

    public static OpenApiDocument WithSecurityRequirement(this OpenApiDocument document, OpenApiSecurityScheme scheme)
    {
        ArgumentNullException.ThrowIfNull(document);

        document.SecurityRequirements ??= new List<OpenApiSecurityRequirement>();
        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
        {
            [scheme] = new List<String>(),
        });

        return document;
    }

    public static OpenApiDocument WithSecuritySchemeComponent(this OpenApiDocument document, String name, OpenApiSecurityScheme? scheme = null)
    {
        document
            .WithComponents();

        document.Components
            .WithSecurityScheme(name, scheme ?? new OpenApiSecurityScheme());

        return document;
    }

    public static OpenApiDocument WithServer(this OpenApiDocument document, OpenApiServer server)
    {
        ArgumentNullException.ThrowIfNull(document);

        document.Servers ??= new List<OpenApiServer>();
        document.Servers.Add(server);

        return document;
    }
}
