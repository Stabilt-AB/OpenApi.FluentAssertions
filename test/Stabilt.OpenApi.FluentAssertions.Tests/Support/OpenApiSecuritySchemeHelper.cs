using System.Diagnostics.CodeAnalysis;

namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiSecuritySchemeHelper
{
    public static OpenApiSecurityScheme Create(
        SecuritySchemeType type = SecuritySchemeType.Http)
    {
        var scheme = new OpenApiSecurityScheme
        {
            Type = type,
        };

        return scheme;
    }

    public static OpenApiSecurityScheme CreateApiKeyScheme(
        String name = "test-api-key",
        ParameterLocation location = ParameterLocation.Header)
    {
        var scheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.ApiKey,
            Name = name,
            In = location,
        };

        return scheme;
    }

    public static OpenApiSecurityScheme CreateJwtBearerScheme(
        String scheme = "bearer",
        String bearerFormat = "JWT")
    {
        return new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = scheme,
            BearerFormat = bearerFormat,
        };
    }

    public static OpenApiSecurityScheme CreateOAuth2ImplicitScheme(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        String authorizationUrl = "https://oauth2.stabilt.dev/authorize",
        IDictionary<String, String>? scopes = null)
    {
        return CreateOAuth2ImplicitScheme(new Uri(authorizationUrl), scopes);
    }

    private static OpenApiSecurityScheme CreateOAuth2ImplicitScheme(
        Uri authorizationUrl,
        IDictionary<String, String>? scopes = null)
    {
        var scheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = authorizationUrl,
                    Scopes = scopes,
                },
            },
        };

        return scheme;
    }

    public static OpenApiSecurityScheme CreateOpenIdConnectScheme(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        String openIdConnectUrl = "https://oidc.stabilt.dev/.well-known/openid-configuration")
    {
        return CreateOpenIdConnectScheme(new Uri(openIdConnectUrl));
    }

    private static OpenApiSecurityScheme CreateOpenIdConnectScheme(
        Uri openIdConnectUrl)
    {
        var scheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OpenIdConnect,
            OpenIdConnectUrl = openIdConnectUrl,
        };

        return scheme;
    }

    public static OpenApiSecurityScheme WithLocation(
        this OpenApiSecurityScheme securityScheme,
        ParameterLocation location)
    {
        ArgumentNullException.ThrowIfNull(securityScheme);

        securityScheme.In = location;

        return securityScheme;
    }

    public static OpenApiSecurityScheme WithOAuth2AuthorizationCodeFlow(
        this OpenApiSecurityScheme scheme,
        OpenApiOAuthFlow flow)
    {
        scheme.WithOAuth2Flows();

        scheme.Flows.AuthorizationCode = flow;

        return scheme;
    }

    public static OpenApiSecurityScheme WithOAuth2ClientCredentialsFlow(
        this OpenApiSecurityScheme scheme,
        OpenApiOAuthFlow flow)
    {
        scheme.WithOAuth2Flows();

        scheme.Flows.ClientCredentials = flow;

        return scheme;
    }

    public static OpenApiSecurityScheme WithOAuth2Flows(
        this OpenApiSecurityScheme scheme)
    {
        ArgumentNullException.ThrowIfNull(scheme);

        scheme.Flows ??= new OpenApiOAuthFlows();

        return scheme;
    }

    public static OpenApiSecurityScheme WithOAuth2ImplicitFlow(
        this OpenApiSecurityScheme scheme,
        OpenApiOAuthFlow flow)
    {
        ArgumentNullException.ThrowIfNull(scheme);

        scheme.WithOAuth2Flows();

        scheme.Flows.Implicit = flow;

        return scheme;
    }

    public static OpenApiSecurityScheme WithOAuth2PasswordFlow(
        this OpenApiSecurityScheme scheme,
        OpenApiOAuthFlow flow)
    {
        ArgumentNullException.ThrowIfNull(scheme);

        scheme.WithOAuth2Flows();

        scheme.Flows.Password = flow;

        return scheme;
    }

    public static OpenApiSecurityScheme WithReference(
        this OpenApiSecurityScheme scheme,
        String id)
    {
        ArgumentNullException.ThrowIfNull(scheme);

        scheme.Reference = new OpenApiReference
        {
            IsFragrament = false,
            ExternalResource = null,
            Type = ReferenceType.SecurityScheme,
            Id = id,
            HostDocument = null,
        };

        return scheme;
    }
}
