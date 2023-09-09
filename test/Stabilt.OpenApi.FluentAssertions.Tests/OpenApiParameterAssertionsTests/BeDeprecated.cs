namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeDeprecated
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_deprecated_Does_not_throw_exception()
    {
        _parameter.WithDeprecated();

        var act = () => _parameter.Should().BeDeprecated();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_deprecated_Throws_exception()
    {
        var act = () => _parameter.Should().BeDeprecated("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _parameter to be deprecated because reasons, but it wasn't.");
    }
}
