namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class OnlyContainSecuritySchemes
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSecuritySchemeComponent("bearer-1")
        .WithSecuritySchemeComponent("bearer-2");

    [Theory]
    [InlineData("bearer-1", "bearer-2")]
    [InlineData("bearer-2", "bearer-1")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _document.Should().OnlyContainSecuritySchemes(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _document.Should().OnlyContainSecuritySchemes(["bearer-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to only contain security schemes {"bearer-1"} because reasons, but found {"bearer-1", "bearer-2"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainSecuritySchemes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainSecuritySchemes(["bearer-1", "bearer-2"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to only contain security schemes {"bearer-1", "bearer-2"} because reasons, but found no security schemes.""");
    }
}
