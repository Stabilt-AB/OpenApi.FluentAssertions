namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainSecurityScheme
{
    private readonly OpenApiDocument _document;

    public ContainSecurityScheme()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.CreateJwtBearerScheme();
        _document = OpenApiDocumentHelper.Create().WithSecuritySchemeComponent("bearer", securityScheme);
    }

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainSecurityScheme("bearer");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _document.Should().ContainSecurityScheme("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain a security scheme "foo" because reasons, but found {"bearer"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainSecurityScheme("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a security scheme "foo" because reasons, but found no security schemes.""");
    }
}
