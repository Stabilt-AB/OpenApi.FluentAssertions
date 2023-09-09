namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainSecurityRequirementReferences
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
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
    public void When_contains_all_Does_not_throw_exception(params string[] expectedValues)
    {
        var act = () => _operation.Should().ContainSecurityRequirementReferences(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _operation.Should().ContainSecurityRequirementReferences([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _operation.Should().ContainSecurityRequirementReferences(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain security requirements {"foo", "bar"} because reasons, but found {"test-security-1", "test-security-2", "test-security-3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _operation.Should().ContainSecurityRequirementReferences(["foo", "test-security-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain security requirements {"foo", "test-security-1"} because reasons, but found {"test-security-1", "test-security-2", "test-security-3"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_for_none_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainSecurityRequirementReferences([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainSecurityRequirementReferences(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain security requirements {"foo", "bar"} because reasons, but found no security requirements.""");
    }
}
