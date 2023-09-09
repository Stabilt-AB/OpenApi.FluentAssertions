namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class OnlyContainPaths
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithPathItem("/items")
        .WithPathItem("/items/{itemId}");

    [Theory]
    [InlineData("/items", "/items/{itemId}")]
    [InlineData("/items/{itemId}", "/items")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] expectedValues)
    {
        var act = () => _document.Should().OnlyContainPaths(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _document.Should().OnlyContainPaths(["/foo", "/bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to only contain paths {"/foo", "/bar"} because reasons, but found {"/items", "/items/{itemId}"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainPaths([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainPaths(["/foo", "/bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to only contain paths {"/foo", "/bar"} because reasons, but found no paths.""");
    }
}
