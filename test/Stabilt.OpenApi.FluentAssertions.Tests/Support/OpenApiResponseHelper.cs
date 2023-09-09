namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiResponseHelper
{
    public static OpenApiResponse Create() => new();

    public static OpenApiResponse WithContent(this OpenApiResponse response, String contentType)
    {
        ArgumentNullException.ThrowIfNull(response);

        response.Content[contentType] = new OpenApiMediaType();

        return response;
    }
}
