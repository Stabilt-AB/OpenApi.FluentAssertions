namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveOperationCount
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithServer(OpenApiServerHelper.Create())
        .WithPathItem("/items", path => path
            .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItems"))
            .WithOperation(OperationType.Post, OpenApiOperationHelper.Create("addItem")))
        .WithPathItem("/items/{itemId}", path => path
            .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItem"))
            .WithOperation(OperationType.Put, OpenApiOperationHelper.Create("updateItem"))
            .WithOperation(OperationType.Delete, OpenApiOperationHelper.Create("deleteItem")))
        .WithSchemaComponent("Item")
        .WithSchemaComponent("Another");

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveOperationCount(5);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveOperationCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected document to have 1337 paths and operations because reasons, but found no operations.");
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveOperationCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have 1337 paths and operations because reasons, but found 5: {"DELETE /items/{itemId}", "GET /items", "GET /items/{itemId}", "POST /items", "PUT /items/{itemId}"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveOperationCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
