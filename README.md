# Stabilt.OpenApi.FluentAssertions

FluentAssertions for Microsoft.OpenApi. Can be used for contract tests.

## Usage

### WebApplicationFactory example

Examples using xUnit.

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

public class OpenApiTestFixture
    : WebApplicationFactory<IWebAssemblyMarker>, IAsyncLifetime
{
    public OpenApiDocument Document { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await using var stream = await CreateClient().GetStreamAsync("/swagger/v1/swagger.json");

        var result = await new OpenApiStreamReader().ReadAsync(stream);

        Document = result.OpenApiDocument;
    }

    public new Task DisposeAsync() => Task.CompletedTask;
}
```

### Test example

```csharp
using Microsoft.OpenApi.Models;
using Stabilt.OpenApi.FluentAssertions;

public class OpenApiDocumentTests(OpenApiTestFixture2 openApi) : IClassFixture<OpenApiTestFixture2>
{
    [Fact]
    public void Document_contains_paths()
    {
        openApi.Document.Should().OnlyContainPaths(
        [
            "/items",
            "/items/{itemId}",
        ]);
    }

    [Theory]
    [InlineData("/v1/items", OperationType.Get, OperationType.Post)]
    [InlineData("/v1/items/{itemId}", OperationType.Get, OperationType.Put, OperationType.Delete)]
    public void Document_contains_operations(String path, params OperationType[] operationTypes)
    {
        openApi.Document.Should().ContainPath(path)
            .Which.Should().OnlyContainOperations(operationTypes);
    }

    [Fact]
    public void Document_contains_GetItem_operation()
    {
        openApi.Document.Should()
            .ContainOperation(OperationType.Get, "/items/{itemId}");
    }

    [Fact]
    public void GetItem_operation_has_itemId_parameter()
    {
        openApi.Document.Should().ContainOperation(OperationType.Get, "/v1/items/{itemId}")
            .Which.Should().ContainParameter("itemId")
            .Which.Should().BeInPath()
            .And.BeUuidType()
            .And.BeRequired();
    }

    [Fact]
    public void Document_contains_schemas()
    {
        openApi.Document.Should().OnlyContainSchemas(
        [
            "Item",
        ]);
    }

    [Fact]
    public void Item_schema_contains_properties()
    {
        openApi.Document.Should().ContainSchema("ItemResponse")
            .Which.Should().OnlyContainProperties(
            [
                "id",
                "name",
            ]);
    }
}
```
