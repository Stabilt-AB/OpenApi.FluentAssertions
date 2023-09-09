using System.Diagnostics.CodeAnalysis;

namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiServerHelper
{
    public static OpenApiServer Create(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        String url = "https://test.stabilt.dev",
        String description = "Test server") => new()
    {
        Description = description,
        Url = url,
        Variables = null,
    };
}
