namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainOperation
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithPathItem("/items/{itemId}", path => path
            .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItem"))
            .WithOperation(OperationType.Put, OpenApiOperationHelper.Create("updateItem"))
            .WithOperation(OperationType.Delete, OpenApiOperationHelper.Create("deleteItem")))
        .WithPathItem("/items", path => path
            .WithOperation(OperationType.Post, OpenApiOperationHelper.Create("addItem"))
            .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItems")));

    [Theory]
    [InlineData("/items", OperationType.Get)]
    [InlineData("/items", OperationType.Post)]
    [InlineData("/items/{itemId}", OperationType.Delete)]
    [InlineData("/items/{itemId}", OperationType.Get)]
    [InlineData("/items/{itemId}", OperationType.Put)]
    public void When_contains_Does_not_throw_exception(String path, OperationType operationType)
    {
        var act = () => _document.Should().ContainOperation(operationType, path);

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(
        "/items",
        OperationType.Trace,
        """Expected _document to contain a path "/items" with operation "Trace" because reasons, but found operations {"Post", "Get"}.""")]
    [InlineData(
        "foo",
        OperationType.Get,
        """Expected _document to contain a path "foo" with operation "Get" because reasons, but found paths {"/items/{itemId}", "/items"}.""")]
    public void When_not_contains_Throws_exception(String path, OperationType operationType, String expectedMessage)
    {
        var act = () => _document.Should().ContainOperation(operationType, path, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void When_paths_is_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainOperation(OperationType.Get, "/items", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a path "/items" with operation "Get" because reasons, but found no paths.""");
    }

    [Fact]
    public void When_operations_is_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create()
            .WithPathItem("/items");

        var act = () => document.Should().ContainOperation(OperationType.Trace, "/items", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a path "/items" with operation "Trace" because reasons, but found no operations.""");
    }
}
