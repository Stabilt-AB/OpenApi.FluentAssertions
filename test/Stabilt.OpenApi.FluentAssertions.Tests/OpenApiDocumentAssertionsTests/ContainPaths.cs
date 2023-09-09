namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainPaths
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithPathItem("/items")
        .WithPathItem("/users")
        .WithPathItem("/groups");

    [Theory]
    [InlineData("/groups")]
    [InlineData("/groups", "/items")]
    [InlineData("/groups", "/items", "/users")]
    [InlineData("/groups", "/users")]
    [InlineData("/groups", "/users", "/items")]
    [InlineData("/items")]
    [InlineData("/items", "/groups")]
    [InlineData("/items", "/groups", "/users")]
    [InlineData("/items", "/users")]
    [InlineData("/items", "/users", "/groups")]
    [InlineData("/users")]
    [InlineData("/users", "/groups")]
    [InlineData("/users", "/groups", "/items")]
    [InlineData("/users", "/items")]
    [InlineData("/users", "/items", "/groups")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _document.Should().ContainPaths(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainPaths([]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _document.Should().ContainPaths(["/foo", "/bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain paths {"/foo", "/bar"} because reasons, but found {"/items", "/users", "/groups"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _document.Should().ContainPaths(["/foo", "/items"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain paths {"/foo", "/items"} because reasons, but found {"/items", "/users", "/groups"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_for_none_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainPaths([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_components_is_null_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainPaths(["/foo", "/bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain paths {"/foo", "/bar"} because reasons, but found no paths.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create()
            .WithComponents();

        var act = () => document.Should().ContainPaths(["/foo", "/bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain paths {"/foo", "/bar"} because reasons, but found no paths.""");
    }
}
