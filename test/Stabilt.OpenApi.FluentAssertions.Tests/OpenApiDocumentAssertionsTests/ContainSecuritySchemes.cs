namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainSecuritySchemes
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSecuritySchemeComponent("bearer-1")
        .WithSecuritySchemeComponent("bearer-2")
        .WithSecuritySchemeComponent("bearer-3");

    [Theory]
    [InlineData("bearer-1")]
    [InlineData("bearer-1", "bearer-2")]
    [InlineData("bearer-1", "bearer-2", "bearer-3")]
    [InlineData("bearer-1", "bearer-3")]
    [InlineData("bearer-1", "bearer-3", "bearer-2")]
    [InlineData("bearer-2")]
    [InlineData("bearer-2", "bearer-1")]
    [InlineData("bearer-2", "bearer-1", "bearer-3")]
    [InlineData("bearer-2", "bearer-3")]
    [InlineData("bearer-2", "bearer-3", "bearer-1")]
    [InlineData("bearer-3")]
    [InlineData("bearer-3", "bearer-1")]
    [InlineData("bearer-3", "bearer-1", "bearer-2")]
    [InlineData("bearer-3", "bearer-2")]
    [InlineData("bearer-3", "bearer-2", "bearer-1")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _document.Should().ContainSecuritySchemes(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainSecuritySchemes([]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _document.Should().ContainSecuritySchemes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain security schemes {"foo", "bar"} because reasons, but found {"bearer-1", "bearer-2", "bearer-3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _document.Should().ContainSecuritySchemes(["foo", "bearer-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain security schemes {"foo", "bearer-1"} because reasons, but found {"bearer-1", "bearer-2", "bearer-3"}.""");
    }

    [Fact]
    public void When_components_is_null_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainSecuritySchemes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain security schemes {"foo", "bar"} because reasons, but found no security schemes.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create()
            .WithComponents();

        var act = () => document.Should().ContainSecuritySchemes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain security schemes {"foo", "bar"} because reasons, but found no security schemes.""");
    }
}
