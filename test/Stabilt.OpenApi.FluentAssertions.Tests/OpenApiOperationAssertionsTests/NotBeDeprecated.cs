namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class NotBeDeprecated
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

    [Fact]
    public void When_not_deprecated_Does_not_throw_exception()
    {
        var act = () => _operation.Should().NotBeDeprecated();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_deprecated_Throws_exception()
    {
        _operation
            .WithDeprecated();

        var act = () => _operation.Should().NotBeDeprecated("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _operation to not be deprecated because reasons, but it was.");
    }
}
