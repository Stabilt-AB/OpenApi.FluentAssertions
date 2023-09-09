namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainSecurityRequirementReferences
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-1"))
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-2"))
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-3"));

    [Theory]
    [InlineData("test-security-1")]
    [InlineData("test-security-1", "test-security-2")]
    [InlineData("test-security-1", "test-security-2", "test-security-3")]
    [InlineData("test-security-1", "test-security-3")]
    [InlineData("test-security-1", "test-security-3", "test-security-2")]
    [InlineData("test-security-2")]
    [InlineData("test-security-2", "test-security-1")]
    [InlineData("test-security-2", "test-security-1", "test-security-3")]
    [InlineData("test-security-2", "test-security-3")]
    [InlineData("test-security-2", "test-security-3", "test-security-1")]
    [InlineData("test-security-3")]
    [InlineData("test-security-3", "test-security-1")]
    [InlineData("test-security-3", "test-security-1", "test-security-2")]
    [InlineData("test-security-3", "test-security-2")]
    [InlineData("test-security-3", "test-security-2", "test-security-1")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _document.Should().ContainSecurityRequirementReferences(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainSecurityRequirementReferences([]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _document.Should().ContainSecurityRequirementReferences(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain security requirements {"foo", "bar"} because reasons, but found {"test-security-1", "test-security-2", "test-security-3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _document.Should().ContainSecurityRequirementReferences(["foo", "test-security-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain security requirements {"foo", "test-security-1"} because reasons, but found {"test-security-1", "test-security-2", "test-security-3"}.""");
    }

    [Fact]
    public void When_components_is_null_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainSecurityRequirementReferences(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain security requirements {"foo", "bar"} because reasons, but found no security requirements.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create()
            .WithComponents();

        var act = () => document.Should().ContainSecurityRequirementReferences(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain security requirements {"foo", "bar"} because reasons, but found no security requirements.""");
    }
}
