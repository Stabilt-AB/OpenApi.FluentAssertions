namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class OnlyContainSecurityRequirementReferences
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-1"))
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-2"));

    [Theory]
    [InlineData("test-security-1", "test-security-2")]
    [InlineData("test-security-2", "test-security-1")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] securityRequirements)
    {
        var act = () => _document.Should().OnlyContainSecurityRequirementReferences(securityRequirements);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _document.Should().OnlyContainSecurityRequirementReferences(["test-security-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to only contain security requirements {"test-security-1"} because reasons, but found {"test-security-1", "test-security-2"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainSecurityRequirementReferences([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainSecurityRequirementReferences(["test-security-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to only contain security requirements {"test-security-1"} because reasons, but found no security requirements.""");
    }
}
