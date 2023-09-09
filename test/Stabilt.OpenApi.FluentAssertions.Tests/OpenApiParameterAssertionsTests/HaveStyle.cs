namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveStyle
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_has_style_Does_not_throw_exception()
    {
        var act = () => _parameter.Should().HaveStyle(ParameterStyle.Simple);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_style_is_wrong_Throws_exception()
    {
        var act = () => _parameter.Should().HaveStyle(ParameterStyle.Matrix, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have style "Matrix" because reasons, but found "Simple".""");
    }
}
