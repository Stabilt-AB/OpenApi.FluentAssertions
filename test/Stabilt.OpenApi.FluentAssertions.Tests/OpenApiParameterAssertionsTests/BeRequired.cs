namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeRequired
{
    [Fact]
    public void When_parameter_is_required_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create(required: true);

        var act = () => parameter.Should().BeRequired();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_required_Throws_exception()
    {
        var parameter = OpenApiParameterHelper.Create();

        var act = () => parameter.Should().BeRequired("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected parameter to be required because reasons, but it wasn't.");
    }
}
