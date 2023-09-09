namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainSecurityRequirementReference
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-1"))
        .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security-2"));

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _operation.Should().ContainSecurityRequirementReference("test-security-1");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _operation.Should().ContainSecurityRequirementReference("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain a security requirement "foo" because reasons, but found {"test-security-1", "test-security-2"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainSecurityRequirementReference("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain a security requirement "foo" because reasons, but found no security requirements.""");
    }
}
