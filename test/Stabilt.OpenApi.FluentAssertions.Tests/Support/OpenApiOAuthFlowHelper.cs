using System.Diagnostics.CodeAnalysis;

namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiOAuthFlowHelper
{
    public static OpenApiOAuthFlow Create() => new();

    public static OpenApiOAuthFlow WithAuthorizationUrl(this OpenApiOAuthFlow flow, [StringSyntax(StringSyntaxAttribute.Uri)] String authorizationUrl) =>
        WithAuthorizationUrl(flow, new Uri(authorizationUrl));

    private static OpenApiOAuthFlow WithAuthorizationUrl(this OpenApiOAuthFlow flow, Uri authorizationUrl)
    {
        ArgumentNullException.ThrowIfNull(flow);

        flow.AuthorizationUrl = authorizationUrl;

        return flow;
    }

    public static OpenApiOAuthFlow WithRefreshUrl(this OpenApiOAuthFlow flow, [StringSyntax(StringSyntaxAttribute.Uri)] String refreshUrl) =>
        WithRefreshUrl(flow, new Uri(refreshUrl));

    private static OpenApiOAuthFlow WithRefreshUrl(this OpenApiOAuthFlow flow, Uri refreshUrl)
    {
        ArgumentNullException.ThrowIfNull(flow);

        flow.RefreshUrl = refreshUrl;

        return flow;
    }

    public static OpenApiOAuthFlow WithScopes(this OpenApiOAuthFlow flow, params String[] scopes)
    {
        return flow.WithScopes(scopes.ToDictionary(s => s, _ => "scope"));
    }

    public static OpenApiOAuthFlow WithScopes(this OpenApiOAuthFlow flow, IDictionary<String, String> scopes)
    {
        ArgumentNullException.ThrowIfNull(flow);

        flow.Scopes = scopes;

        return flow;
    }

    public static OpenApiOAuthFlow WithTokenUrl(this OpenApiOAuthFlow flow, [StringSyntax(StringSyntaxAttribute.Uri)] String tokenUrl) =>
        WithTokenUrl(flow, new Uri(tokenUrl));

    private static OpenApiOAuthFlow WithTokenUrl(this OpenApiOAuthFlow flow, Uri tokenUrl)
    {
        ArgumentNullException.ThrowIfNull(flow);

        flow.TokenUrl = tokenUrl;

        return flow;
    }
}
