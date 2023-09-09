namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class NotBeRequired
{
    [Fact]
    public void When_not_required_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create();

        var act = () => parameter.Should().NotBeRequired();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_parameter_is_required_Throws_exception()
    {
        var parameter = OpenApiParameterHelper.Create(required: true);

        var act = () => parameter.Should().NotBeRequired("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected parameter to not be required because reasons, but it was.");
    }
}
