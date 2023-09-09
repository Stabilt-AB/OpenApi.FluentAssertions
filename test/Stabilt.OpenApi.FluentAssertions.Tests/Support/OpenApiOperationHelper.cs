namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiOperationHelper
{
    public static OpenApiOperation Create(String operationId = "myOperation") => new()
    {
        OperationId = operationId,
        Summary = null,
        Description = null,
        ExternalDocs = null,
        Callbacks = null,
        Servers = null,
        RequestBody = null,
    };

    public static OpenApiOperation WithDeprecated(this OpenApiOperation operation, Boolean deprecated = true)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Deprecated = deprecated;

        return operation;
    }

    public static OpenApiOperation WithParameter(this OpenApiOperation operation, String name) =>
        operation.WithParameter(OpenApiParameterHelper.Create(name));

    public static OpenApiOperation WithParameter(this OpenApiOperation operation, OpenApiParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(parameter);

        return operation;
    }

    public static OpenApiOperation WithRequestBody(this OpenApiOperation operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.RequestBody ??= new OpenApiRequestBody();

        return operation;
    }

    public static OpenApiOperation WithRequestBodyContent(this OpenApiOperation operation, String contentType, OpenApiMediaType requestBody)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.RequestBody ??= new OpenApiRequestBody();

        operation.RequestBody.Content[contentType] = requestBody;

        return operation;
    }

    public static OpenApiOperation WithResponses(this OpenApiOperation operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Responses ??= new OpenApiResponses();

        return operation;
    }

    public static OpenApiOperation WithResponseCode(this OpenApiOperation operation, Int32 statusCode, OpenApiResponse response) =>
        WithResponseCode(operation, $"{statusCode}", response);

    public static OpenApiOperation WithResponseCode(this OpenApiOperation operation, String statusCode, OpenApiResponse response)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Responses ??= new OpenApiResponses();

        operation.Responses[statusCode] = response;

        return operation;
    }

    public static OpenApiOperation WithSecurityRequirement(this OpenApiOperation operation, OpenApiSecurityScheme scheme)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Security ??= new List<OpenApiSecurityRequirement>();

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [scheme] = new List<String>(),
        });

        return operation;
    }

    public static OpenApiOperation WithTags(this OpenApiOperation operation, params String[] tags)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(tags);

        operation.Tags ??= new List<OpenApiTag>();

        foreach (var name in tags)
        {
            operation.Tags.Add(new OpenApiTag
            {
                Name = name,
                Description = null,
                ExternalDocs = null,
                Extensions = null,
                UnresolvedReference = false,
            });
        }

        return operation;
    }
}
