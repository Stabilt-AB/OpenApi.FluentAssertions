namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class NotContainSecurityRequirementReferences
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _operation.Should().NotContainSecurityRequirementReferences();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _operation
            .WithSecurityRequirement(OpenApiSecuritySchemeHelper.CreateJwtBearerScheme().WithReference("test-security"));

        var act = () => _operation.Should().NotContainSecurityRequirementReferences("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to not contain any security requirements because reasons, but found {"test-security"}.""");
    }
}
