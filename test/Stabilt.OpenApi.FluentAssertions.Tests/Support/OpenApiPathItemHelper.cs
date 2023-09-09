namespace Stabilt.OpenApi.FluentAssertions.Tests.Support;

public static class OpenApiPathItemHelper
{
    public static OpenApiPathItem Create() => new()
    {
        Summary = null,
        Description = null,
        Operations = new Dictionary<OperationType, OpenApiOperation>(),
        Servers = null,
        Parameters = null,
        UnresolvedReference = false,
        Reference = null,
    };

    public static OpenApiPathItem WithOperation(this OpenApiPathItem pathItem, OperationType operationType, OpenApiOperation operation)
    {
        ArgumentNullException.ThrowIfNull(pathItem);

        pathItem.Operations[operationType] = operation;

        return pathItem;
    }
}
