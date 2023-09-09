using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public static class OpenApiAssertionsExtensions
{
    public static OpenApiComponentsAssertions Should(this OpenApiComponents components) => new(components);

    public static OpenApiDocumentAssertions Should(this OpenApiDocument document) => new(document);

    public static OpenApiOAuthFlowAssertions Should(this OpenApiOAuthFlow flow) => new(flow);

    public static OpenApiOperationAssertions Should(this OpenApiOperation operation) => new(operation);

    public static OpenApiParameterAssertions Should(this OpenApiParameter parameter) => new(parameter);

    public static OpenApiPathItemAssertions Should(this OpenApiPathItem pathItem) => new(pathItem);

    public static OpenApiResponseAssertions Should(this OpenApiResponse response) => new(response);

    public static OpenApiSchemaAssertions Should(this OpenApiSchema schema) => new(schema);

    public static OpenApiSecuritySchemeAssertions Should(this OpenApiSecurityScheme securityScheme) => new(securityScheme);

    public static OpenApiServerAssertions Should(this OpenApiServer server) => new(server);
}
