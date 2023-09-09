namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class BeDeprecated
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

    [Fact]
    public void When_deprecated_Does_not_throw_exception()
    {
        var operation = _operation.WithDeprecated();

        var act = () => operation.Should().BeDeprecated();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_deprecated_Throws_exception()
    {
        var act = () => _operation.Should().BeDeprecated("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _operation to be deprecated because reasons, but it wasn't.");
    }
}
