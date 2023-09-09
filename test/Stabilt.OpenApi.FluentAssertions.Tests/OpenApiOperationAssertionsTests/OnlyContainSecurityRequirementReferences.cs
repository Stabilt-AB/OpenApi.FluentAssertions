namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class OnlyContainSecurityRequirementReferences
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-1"))
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-2"));

    [Theory]
    [InlineData("test-security-1", "test-security-2")]
    [InlineData("test-security-2", "test-security-1")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] securityRequirements)
    {
        var act = () => _operation.Should().OnlyContainSecurityRequirementReferences(securityRequirements);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainSecurityRequirementReferences(["test-security-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain security requirements {"test-security-1"} because reasons, but found {"test-security-1", "test-security-2"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainSecurityRequirementReferences(["test-security-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain security requirements {"test-security-1"} because reasons, but found no security requirements.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainSecurityRequirementReferences([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
