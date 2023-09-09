namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainSchema
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSchemaComponent("Item")
        .WithSchemaComponent("Another");

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainSchema("Item");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _document.Should().ContainSchema("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain a schema "Foo" because reasons, but found {"Item", "Another"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainSchema("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a schema "Foo" because reasons, but found no schemas.""");
    }
}
