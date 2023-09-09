namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class NotBeDeprecated
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_not_deprecated_Does_not_throw_exception()
    {
        var act = () => _parameter.Should().NotBeDeprecated();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_deprecated_Throws_exception()
    {
        _parameter.WithDeprecated();

        var act = () => _parameter.Should().NotBeDeprecated("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _parameter to not be deprecated because reasons, but it was.");
    }
}
