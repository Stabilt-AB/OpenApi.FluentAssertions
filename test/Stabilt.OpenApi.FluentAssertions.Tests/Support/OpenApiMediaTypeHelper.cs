namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiMediaTypeHelper
{
    public static OpenApiMediaType Create() => new()
    {
        Schema = null,
        Example = null,
        Examples = null,
        Encoding = null,
    };
}
