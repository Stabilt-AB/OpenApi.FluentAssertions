namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainPath
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithPathItem("/items")
        .WithPathItem("/items/{itemId}");

    [Theory]
    [InlineData("/items")]
    [InlineData("/items/{itemId}")]
    public void When_contains_Does_not_throw_exception(String path)
    {
        var act = () => _document.Should().ContainPath(path);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _document.Should().ContainPath("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain a path "foo" because reasons, but found {"/items", "/items/{itemId}"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainPath("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a path "foo" because reasons, but found no paths.""");
    }
}
