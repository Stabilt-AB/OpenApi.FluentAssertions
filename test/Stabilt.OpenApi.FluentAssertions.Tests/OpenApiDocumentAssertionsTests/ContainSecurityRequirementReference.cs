namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainSecurityRequirementReference
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme()
            .WithReference("test-requirement"));

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainSecurityRequirementReference("test-requirement");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _document.Should().ContainSecurityRequirementReference("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain a security requirement "foo" because reasons, but found {"test-requirement"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainSecurityRequirementReference("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a security requirement "foo" because reasons, but found no security requirements.""");
    }
}
